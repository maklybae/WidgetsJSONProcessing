using System.Text.Json;
using WidgetsJSON.AutoSaverEvent;

namespace WidgetsJSON;

public abstract class JSONDataType
{
    protected JSONDataType() { }

    public event EventHandler<UpdatedEventArgs>? Updated;

    public string ToJSON() =>
        JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

    protected virtual void OnUpdated(UpdatedEventArgs e)
    {
        Updated?.Invoke(this, e);
    }
}
