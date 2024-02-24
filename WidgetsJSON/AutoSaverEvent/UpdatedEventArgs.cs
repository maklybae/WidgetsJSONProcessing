namespace WidgetsJSON.AutoSaverEvent;

/// <summary>
/// Provides event arguments for the updated event.
/// </summary>
public class UpdatedEventArgs : EventArgs
{
    private readonly DateTime _happened;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatedEventArgs"/> class with the current timestamp.
    /// </summary>
    public UpdatedEventArgs() =>
        _happened = DateTime.Now;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatedEventArgs"/> class with a specified timestamp.
    /// </summary>
    /// <param name="happened">The timestamp when the event occurred.</param>
    public UpdatedEventArgs(DateTime happened) =>
        _happened = happened;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Happened => _happened;
}
