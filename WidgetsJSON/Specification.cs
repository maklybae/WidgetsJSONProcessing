﻿using System.Text.Json.Serialization;

namespace WidgetsJSON
{
    public class Specification : JSONDataType
    {
        private string? _name;
        private double? _price;
        private bool? _isCustom;
        private Specification() { }

        public Specification(string? name, double? price, bool? isCustom)
        {
            Name = name;
            Price = price;
            IsCustom = isCustom;
        }

        [JsonPropertyName("specName")]
        public string? Name { get { return _name; } private set { _name = value; } }

        [JsonPropertyName("specPrice")]
        public double? Price { get { return _price; } private set { _price = value; } }

        [JsonPropertyName("isCustom")]
        public bool? IsCustom { get { return _isCustom; } private set { _isCustom = value; } }

    }
}
