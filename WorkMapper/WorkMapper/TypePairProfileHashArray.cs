namespace WorkMapper
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Runtime.CompilerServices;

    [DebuggerDisplay("{" + nameof(Diagnostics) + "}")]
    internal sealed class TypePairProfileHashArray
    {
        private const int InitialSize = 256;

        private const int Factor = 3;

        private static readonly Node EmptyNode = new(typeof(EmptyKey), typeof(EmptyKey), string.Empty, default!);

        private readonly object sync = new();

        private Node[] nodes;

        private int depth;

        private int count;

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public TypePairProfileHashArray()
        {
            nodes = CreateInitialTable();
        }

        //--------------------------------------------------------------------------------
        // Private
        //--------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateHash(Type sourceType, Type targetType, string profile)
        {
            unchecked
            {
                return sourceType.GetHashCode() ^ (targetType.GetHashCode() * 397) ^ profile.GetHashCode();
            }
        }

        private static int CalculateDepth(Node node)
        {
            var length = 1;
            var next = node.Next;
            while (next is not null)
            {
                length++;
                next = next.Next;
            }

            return length;
        }

        private static int CalculateDepth(Node[] targetNodes)
        {
            var depth = 0;

            for (var i = 0; i < targetNodes.Length; i++)
            {
                var node = targetNodes[i];
                if (node != EmptyNode)
                {
                    depth = Math.Max(CalculateDepth(node), depth);
                }
            }

            return depth;
        }

        private static int CalculateSize(int requestSize)
        {
            uint size = 0;

            for (var i = 1L; i < requestSize; i *= 2)
            {
                size = (size << 1) + 1;
            }

            return (int)(size + 1);
        }

        private static Node[] CreateInitialTable()
        {
            var newNodes = new Node[InitialSize];

            for (var i = 0; i < newNodes.Length; i++)
            {
                newNodes[i] = EmptyNode;
            }

            return newNodes;
        }

        private static Node FindLastNode(Node node)
        {
            while (node.Next is not null)
            {
                node = node.Next;
            }

            return node;
        }

        private static void UpdateLink(ref Node node, Node addNode)
        {
            if (node == EmptyNode)
            {
                node = addNode;
            }
            else
            {
                var last = FindLastNode(node);
                last.Next = addNode;
            }
        }

        private static void RelocateNodes(Node[] nodes, Node[] oldNodes)
        {
            for (var i = 0; i < oldNodes.Length; i++)
            {
                var node = oldNodes[i];
                if (node == EmptyNode)
                {
                    continue;
                }

                do
                {
                    var next = node.Next;
                    node.Next = null;

                    UpdateLink(ref nodes[CalculateHash(node.SourceType, node.TargetType, node.Profile) & (nodes.Length - 1)], node);

                    node = next;
                }
                while (node is not null);
            }
        }

        private void AddNode(Node node)
        {
            var requestSize = Math.Max(InitialSize, (count + 1) * Factor);
            var size = CalculateSize(requestSize);
            if (size > nodes.Length)
            {
                var newNodes = new Node[size];
                for (var i = 0; i < newNodes.Length; i++)
                {
                    newNodes[i] = EmptyNode;
                }

                RelocateNodes(newNodes, nodes);

                UpdateLink(ref newNodes[CalculateHash(node.SourceType, node.TargetType, node.Profile) & (newNodes.Length - 1)], node);

                Interlocked.MemoryBarrier();

                nodes = newNodes;
                depth = CalculateDepth(newNodes);
                count++;
            }
            else
            {
                Interlocked.MemoryBarrier();

                UpdateLink(ref nodes[CalculateHash(node.SourceType, node.TargetType, node.Profile) & (nodes.Length - 1)], node);

                depth = Math.Max(CalculateDepth(nodes[CalculateHash(node.SourceType, node.TargetType, node.Profile) & (nodes.Length - 1)]), depth);
                count++;
            }
        }

        //--------------------------------------------------------------------------------
        // Public
        //--------------------------------------------------------------------------------

        public DiagnosticsInfo Diagnostics
        {
            get
            {
                lock (sync)
                {
                    return new DiagnosticsInfo(nodes.Length, depth, count);
                }
            }
        }

        public void Clear()
        {
            lock (sync)
            {
                var newNodes = CreateInitialTable();

                Interlocked.MemoryBarrier();

                nodes = newNodes;
                depth = 0;
                count = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(Type sourceType, Type targetType, string profile, [MaybeNullWhen(false)] out object? item)
        {
            var temp = nodes;
            var node = temp[CalculateHash(sourceType, targetType, profile) & (temp.Length - 1)];
            do
            {
                if ((node.SourceType == sourceType) && (node.TargetType == targetType) && (node.Profile == profile))
                {
                    item = node.Item;
                    return true;
                }
                node = node.Next;
            }
            while (node is not null);

            item = default;
            return false;
        }

        public object AddIfNotExist(Type sourceType, Type targetType, string profile, Func<Type, Type, string, object> valueFactory)
        {
            lock (sync)
            {
                // Double checked locking
                if (TryGetValue(sourceType, targetType, profile, out var currentValue))
                {
                    return currentValue!;
                }

                var value = valueFactory(sourceType, targetType, profile);

                // Check if added by recursive
                if (TryGetValue(sourceType, targetType, profile, out currentValue))
                {
                    return currentValue!;
                }

                AddNode(new Node(sourceType, targetType, profile, value));

                return value;
            }
        }

        //--------------------------------------------------------------------------------
        // Inner
        //--------------------------------------------------------------------------------

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Framework only")]
        private sealed class EmptyKey
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Performance")]
        private sealed class Node
        {
            public readonly Type SourceType;

            public readonly Type TargetType;

            public readonly string Profile;

            public readonly object Item;

            public Node? Next;

            public Node(Type sourceType, Type targetType, string profile, object item)
            {
                SourceType = sourceType;
                TargetType = targetType;
                Profile = profile;
                Item = item;
            }
        }

        //--------------------------------------------------------------------------------
        // Diagnostics
        //--------------------------------------------------------------------------------

        public sealed class DiagnosticsInfo
        {
            public int Width { get; }

            public int Depth { get; }

            public int Count { get; }

            public DiagnosticsInfo(int width, int depth, int count)
            {
                Width = width;
                Depth = depth;
                Count = count;
            }

            public override string ToString() => $"Count={Count}, Width={Width}, Depth={Depth}";
        }
    }
}
