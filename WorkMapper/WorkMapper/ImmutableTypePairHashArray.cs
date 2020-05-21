namespace WorkMapper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [DebuggerDisplay("{" + nameof(Count) + "}")]
    public sealed class ImmutableTypePairHashArray<T>
    {
        private const int Factor = 3;

        private static readonly Node EmptyNode = new Node(typeof(EmptyKey), typeof(EmptyKey), default);

        private readonly Node[] nodes;

        public int Count { get; }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public ImmutableTypePairHashArray(ICollection<Tuple<Type, Type, T>> source)
        {
            var size = CalculateSize(source.Count * Factor);
            nodes = new Node[size];
            for (var i = 0; i < nodes.Length; i++)
            {
                nodes[i] = EmptyNode;
            }

            foreach (var entry in source)
            {
                var node = new Node(entry.Item1, entry.Item2, entry.Item3);
                UpdateLink(ref nodes[CalculateHash(node.SourceType, node.DestinationType) & (nodes.Length - 1)], node);
            }

            Count = source.Count;
        }

        //--------------------------------------------------------------------------------
        // Private
        //--------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateHash(Type sourceType, Type targetType)
        {
            unchecked
            {
                return sourceType.GetHashCode() ^ (targetType.GetHashCode() * 397);
            }
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

        private static Node FindLastNode(Node node)
        {
            while (node.Next != null)
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

        //--------------------------------------------------------------------------------
        // Public
        //--------------------------------------------------------------------------------

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(Type sourceType, Type targetType, out T item)
        {
            var temp = nodes;
            var node = temp[CalculateHash(sourceType, targetType) & (temp.Length - 1)];
            do
            {
                if ((node.SourceType == sourceType) && (node.DestinationType == targetType))
                {
                    item = node.Item;
                    return true;
                }
                node = node.Next;
            }
            while (node != null);

            item = default;
            return false;
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

            public readonly Type DestinationType;

            public readonly T Item;

            public Node Next;

            public Node(Type sourceType, Type targetType, T item)
            {
                SourceType = sourceType;
                DestinationType = targetType;
                Item = item;
            }
        }
    }
}
