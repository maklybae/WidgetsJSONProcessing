using System.Text.Json;
using System.Text.Json.Serialization;
using WidgetsJSON.AutoSaverEvent;
using WidgetsJSON.PriceChangedEvent;

namespace WidgetsJSON
{
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="isAvailable"></param>
        /// <param name="manufactureDate"></param>
        /// <param name="specifications"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="JsonException"></exception>
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

        public event EventHandler<PriceChangedEventArgs>? PriceChanged;

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
                OnPriceChanged(new PriceChangedEventArgs(value - _price));
                OnUpdated(new UpdatedEventArgs());
                _price = value; 
            }
        }

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

        // Использовать копию для стороннего доступа, внутри -- поле.
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

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
    }
}