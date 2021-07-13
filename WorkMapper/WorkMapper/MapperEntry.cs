using WorkMapper.Options;

namespace WorkMapper
{
    internal sealed class MapperEntry
    {
        public string? Profile { get; }

        public MapperOption Option { get; }

        public MapperEntry(string? profile, MapperOption option)
        {
            Profile = profile;
            Option = option;
        }
    }
}
