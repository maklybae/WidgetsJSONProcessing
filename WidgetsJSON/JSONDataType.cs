using System.Text.Json;
using WidgetsJSON.AutoSaverEvent;

namespace WidgetsJSON;

/// <summary>
/// An abstract class representing data types that can be serialized to JSON.
/// </summary>
public abstract class JSONDataType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JSONDataType"/> class.
    /// </summary>
    protected JSONDataType() { }

    /// <summary>
    /// Event raised when the data is updated.
    /// </summary>
    public event EventHandler<UpdatedEventArgs>? Updated;


    /// <summary>
    /// Converts the object to a JSON string.
    /// </summary>
    /// <returns>A JSON string representation of the object.</returns>
    public string ToJSON() =>
        JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

    /// <summary>
    /// Raises the Updated event.
    /// </summary>
    /// <param name="e">Event arguments containing information about the update.</param>
    protected virtual void OnUpdated(UpdatedEventArgs e)
    {
        Updated?.Invoke(this, e);
    }
}
