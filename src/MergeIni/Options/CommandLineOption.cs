namespace MergeIni.Options
{
    struct CommandLineOption
    {
        public int Order { get; init; }
        public string Key { get; init; }
        public string? Value { get; init; }
    }
}
