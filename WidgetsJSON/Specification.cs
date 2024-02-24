using System.Text.Json;
using System.Text.Json.Serialization;
using WidgetsJSON.AutoSaverEvent;

namespace WidgetsJSON;

/// <summary>
/// Represents a specification for a widget, derived from <see cref="JSONDataType"/>.
/// </summary>
public class Specification : JSONDataType
{
    private string _name = string.Empty;
    private double _price = default;
    private bool _isCustom = default;
    private Specification() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Specification"/> class with specified values.
    /// </summary>
    /// <param name="name">The name of the specification.</param>
    /// <param name="price">The price of the specification.</param>
    /// <param name="isCustom">A flag indicating whether the specification is custom.</param>
    /// <exception cref="JsonException">Thrown when there is an issue with JSON processing.</exception>
    /// <exception cref="ArgumentException">Thrown when input values are invalid.</exception>
    public Specification(string name, double price, bool? isCustom)
    {
        Name = name;
        Price = price;
        IsCustom = isCustom;
    }

    /// <summary>
    /// Gets or sets the name of the specification.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the price of the specification.
    /// </summary>
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

    /// <summary>
    /// Gets or sets a value indicating whether the specification is custom.
    /// </summary>
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
