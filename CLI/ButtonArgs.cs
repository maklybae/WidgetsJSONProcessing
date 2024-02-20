namespace CLI;

internal class ButtonArgs
{
    private ButtonArgs() { }
    public ButtonArgs(string name)
    {
        Name = name;
    }

    public string Name { get; set; } = string.Empty;
}
