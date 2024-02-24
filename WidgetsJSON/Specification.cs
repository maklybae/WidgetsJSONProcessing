using System.Text.Json;
using System.Text.Json.Serialization;
using WidgetsJSON.AutoSaverEvent;

namespace WidgetsJSON;

public class Specification : JSONDataType
{
    private string _name = string.Empty;
    private double _price = default;
    private bool _isCustom = default;
    private Specification() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="price"></param>
    /// <param name="isCustom"></param>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Specification(string name, double price, bool? isCustom)
    {
        Name = name;
        Price = price;
        IsCustom = isCustom;
    }

    [JsonPropertyName("specName")]
    public string Name 
    { 
        get => _name;
        set 
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(value));
            }
            OnUpdated(new UpdatedEventArgs());
            _name = value;
        }
    }

    [JsonPropertyName("specPrice")]
    public double Price 
    { 
        get => _price; 
        set 
        { 
            if (value <= 0)
            {
                throw new ArgumentException("Price should be greater than zero", nameof(value));
            }
            OnUpdated(new UpdatedEventArgs());
            _price = value;
        } 
    }

    [JsonPropertyName("isCustom")]
    public bool? IsCustom 
    {
        get => _isCustom;
        set 
        {
            if (value == null)
            {
                throw new ArgumentException("Custom flag should have a value (true/false)", nameof(value));
            }
            OnUpdated(new UpdatedEventArgs());
            _isCustom = (bool)value;
        } 
    }
}
