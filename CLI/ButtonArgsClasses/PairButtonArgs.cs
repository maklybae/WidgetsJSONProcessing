namespace CLI.ButtonArgsClasses;

/// <summary>
/// Represents button event arguments with additional information.
/// </summary>
internal class PairButtonArgs : ButtonArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PairButtonArgs"/> class with specified additional information and name.
    /// </summary>
    /// <param name="additionalInfo">Additional information associated with the button.</param>
    /// <param name="name">The name associated with the button.</param>
    public PairButtonArgs(string additionalInfo, string name)
    {
        AdditionalInfo = additionalInfo;
        Name = name;
    }

    /// <summary>
    /// Gets or sets the additional information associated with the button.
    /// </summary>
    public string AdditionalInfo { get; set; } = string.Empty;

    /// <summary>
    /// Returns a string representation of the <see cref="PairButtonArgs"/> object.
    /// </summary>
    /// <returns>The string to output info about button.</returns>
    public override string ToString() =>
        $"{AdditionalInfo} : {Name}";
}
