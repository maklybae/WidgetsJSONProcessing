namespace CLI.ButtonArgsClasses;

internal class NumberedButtonArgs : ButtonArgs
{
    public NumberedButtonArgs(int number, string name)
    {
        Number = number;
        Name = name;
    }

    public int Number { get; set; } = 0;

    public override string ToString() =>
        $"[{(Number > 0 ? Number : string.Empty)}] {Name}";
}
