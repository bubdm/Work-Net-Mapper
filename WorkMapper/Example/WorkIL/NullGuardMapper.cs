using System;

using WorkMapper;
using WorkMapper.Functions;

namespace WorkIL
{
    public sealed class NullGuardMapper
    {
        public Destination? ClassFunc(Source? source)
        {
            if (source is null)
            {
                return null;
            }

            return new Destination();
        }

        public void ClassFunc(Source? source, Destination? destination)
        {
            if (source is null)
            {
                return;
            }
        }

        public StructDestination? NullableFuncToNullable(StructSource? source)
        {
            if (source is null)
            {
                return null;
            }

            return new StructDestination();
        }

        public StructDestination NullableFuncToStruct(StructSource? source)
        {
            if (source is null)
            {
                return default;
            }

            return new StructDestination();
        }

        public void NullableAction(StructSource? source, StructDestination destination)
        {
            if (source is null)
            {
                return;
            }
        }
    }
}
