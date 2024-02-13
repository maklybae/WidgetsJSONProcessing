using System.Text.Json;

namespace WidgetsJSON
{
    public abstract class JSONDataType
    {
        protected JSONDataType() { }
        public string ToJSON() =>
            JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}
