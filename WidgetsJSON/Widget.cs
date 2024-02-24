using System.Text.Json;
using System.Text.Json.Serialization;
using WidgetsJSON.AutoSaverEvent;
using WidgetsJSON.PriceChangedEvent;

namespace WidgetsJSON;

/// <summary>
/// Represents a widget, derived from <see cref="JSONDataType"/>.
/// </summary>
public class Widget : JSONDataType
{
    private string _id = string.Empty;
    private string _name = string.Empty;
    private int _quantity = default;
    private double _price = default;
    private bool _isAvailable = default;
    private DateTime _manufactureDate = default;
    private List<Specification> _specifications = new();

    private Widget() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Widget"/> class with specified values.
    /// </summary>
    /// <param name="id">The ID of the widget.</param>
    /// <param name="name">The name of the widget.</param>
    /// <param name="quantity">The quantity of the widget.</param>
    /// <param name="price">The price of the widget.</param>
    /// <param name="isAvailable">A flag indicating whether the widget is available.</param>
    /// <param name="manufactureDate">The manufacture date of the widget.</param>
    /// <param name="specifications">The specifications of the widget.</param>
    /// <exception cref="ArgumentException">Thrown when input values are invalid.</exception>
    /// <exception cref="JsonException">Thrown when there is an issue with JSON processing.</exception>
    public Widget(string id, string name, int quantity, double price, bool? isAvailable, DateTime manufactureDate, List<Specification> specifications)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Price = price;
        IsAvailable = isAvailable;
        ManufactureDate = manufactureDate;
        Specifications = specifications;
    }

    /// <summary>
    /// Event triggered when the price of the widget changes.
    /// </summary>
    public event EventHandler<PriceChangedEventArgs>? PriceChanged;

    /// <summary>
    /// Gets or sets the ID of the widget.
    /// </summary>
    [JsonPropertyName("widgetId")]
    public string Id
    { 
        get => _id;
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Id cannot be null or empty", nameof(value));
            }
            _id = value;
        }
    }

    /// <summary>
    /// Gets or sets the name of the widget.
    /// </summary>
    [JsonPropertyName("name")]
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
    /// Gets or sets the quantity of the widget.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity
    { 
        get => _quantity;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Quantity should be greater than zero", nameof(value));
            }
            OnUpdated(new UpdatedEventArgs());
            _quantity = value;
        }
    }

    /// <summary>
    /// Gets or sets the price of the widget.
    /// </summary>
    [JsonPropertyName("price")]
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
            OnPriceChanged(new PriceChangedEventArgs(value - _price));
            _price = value; 
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the widget is available.
    /// </summary>
    [JsonPropertyName("isAvailable")]
    public bool? IsAvailable {
        get => _isAvailable;
        set 
        {
            if (value == null)
            {
                throw new ArgumentException("Available flag should have a value (true/false)", nameof(value));
            }
            OnUpdated(new UpdatedEventArgs());
            _isAvailable = (bool)value; 
        }
    }

    /// <summary>
    /// Gets or sets the manufacture date of the widget.
    /// </summary>
    [JsonPropertyName("manufactureDate")]
    public DateTime ManufactureDate 
    { 
        get => _manufactureDate; 
        set 
        {
            if (value.Year < 1900)
            {
                throw new ArgumentException("The manufacture date should be later than 1990.", nameof(value));
            }
            OnUpdated(new UpdatedEventArgs());
            _manufactureDate = value;
        } 
    }

    /// <summary>
    /// Gets or sets the specifications of the widget.
    /// </summary>
    [JsonPropertyName("specifications")]
    public List<Specification> Specifications 
    {
        get => _specifications;
        set 
        {
            if (value == null || value.Count == 0)
            {
                throw new ArgumentException("Specification list should contain at least one specification", nameof(value));
            }
            _specifications = new(value);
        }
    }

    /// <summary>
    /// Raises the PriceChanged event.
    /// </summary>
    /// <param name="e">Event arguments containing the price difference.</param>
    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }
}