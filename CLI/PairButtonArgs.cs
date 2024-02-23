namespace CLI;

internal class PairButtonArgs : ButtonArgs
{
    public PairButtonArgs(string additionalInfo, string name)
    {
        AdditionalInfo = additionalInfo;
        Name = name;
    }

    public string AdditionalInfo { get; set; } = string.Empty;
}
