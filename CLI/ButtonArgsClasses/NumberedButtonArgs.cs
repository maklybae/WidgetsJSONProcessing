namespace CLI.ButtonArgsClasses;

/// <summary>
/// Represents button event arguments with a number associated.
/// </summary>
internal class NumberedButtonArgs : ButtonArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NumberedButtonArgs"/> class with a specified number and name.
    /// </summary>
    /// <param name="number">The number associated with the button.</param>
    /// <param name="name">The name associated with the button.</param>
    public NumberedButtonArgs(int number, string name)
    {
        Number = number;
        Name = name;
    }

    /// <summary>
    /// Gets or sets the number associated with the button.
    /// </summary>
    public int Number { get; set; } = 0;

    /// <summary>
    /// Returns a string representation of the <see cref="NumberedButtonArgs"/> object.
    /// </summary>
    /// <returns>The string to output info about button.</returns>
    public override string ToString() =>
        $"[{(Number > 0 ? Number : ' '.ToString())}] {Name}";
}
