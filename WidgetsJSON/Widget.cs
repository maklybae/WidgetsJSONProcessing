using System.Text.Json.Serialization;

namespace WidgetsJSON
{
    public class Widget : JSONDataType
    {
        private string? _id;
        private string? _name;
        private int? _quantity;
        private double? _price;
        private bool? _isAvailable;
        private DateTime? _manufactureDate;
        private List<Specification>? _specifications;

        [JsonPropertyName("widgetId")]
        public string? Id { get { return _id; } private set { _id = value; } }

        [JsonPropertyName("name")]
        public string? Name { get { return _name; } private set { _name = value; } }

        [JsonPropertyName("quantity")]
        public int? Quantity { get { return _quantity; } private set { _quantity = value; } }

        [JsonPropertyName("price")]
        public double? Price { get { return _price; } private set { _price = value; } }

        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get { return _isAvailable; } private set { _isAvailable = value; } }

        [JsonPropertyName("manufactureDate")]
        public DateTime? ManufactureDate { get { return _manufactureDate; } private set { _manufactureDate = value; } }

        // Использовать копию для стороннего доступа, внутри -- поле.
        [JsonPropertyName("specifications")]
        public List<Specification>? Specifications { get { return _specifications; } private set { _specifications = value; } }

        private Widget() { }

        public Widget(string? id, string? name, int? quantity, double? price, bool? isAvailable, DateTime? manufactureDate, List<Specification>? specifications)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            IsAvailable = isAvailable;
            ManufactureDate = manufactureDate;
            Specifications = specifications;
        }
    }
}