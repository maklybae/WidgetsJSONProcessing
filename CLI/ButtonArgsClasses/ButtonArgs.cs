namespace CLI.ButtonArgsClasses;

/// <summary>
/// Represents the base class for buttons arguments.
/// </summary>
internal class ButtonArgs
{
    protected ButtonArgs() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonArgs"/> class with a specified name.
    /// </summary>
    /// <param name="name">The name associated with the button.</param>
    public ButtonArgs(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets or sets the name associated with the button.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Returns a string representation of the <see cref="ButtonArgs"/> object.
    /// </summary>
    /// <returns>The string to output info about button.</returns>
    public override string ToString() =>
        Name;
}
