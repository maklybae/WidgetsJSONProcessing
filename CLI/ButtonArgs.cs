namespace CLI;

internal class ButtonArgs
{
    protected ButtonArgs() { }
    public ButtonArgs(string name)
    {
        Name = name;
    }

    public string Name { get; set; } = string.Empty;

    public override string ToString() =>
        Name;
}
