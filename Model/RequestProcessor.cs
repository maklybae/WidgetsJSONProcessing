using WidgetsJSON;

namespace Model;

/// <summary>
/// Processes requests related to widget data and manages interactions with the underlying database.
/// </summary>
public class RequestProcessor
{
    private Database? _database;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestProcessor"/> class.
    /// </summary>
    public RequestProcessor() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestProcessor"/> class with the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the JSON file containing widget data.</param>
    public RequestProcessor(string filePath) =>
        _database = new Database(filePath);

    /// <summary>
    /// Gets a value indicating whether the request processor is ready with a loaded database.
    /// </summary>
    public bool IsReady => _database != null;

    /// <summary>
    /// Sets the database to a new instance based on the provided file path.
    /// </summary>
    /// <param name="filePath">The path to the new JSON file containing widget data.</param>
    public void RebaseProcessor(string filePath) =>
        _database = new Database(filePath);

    /// <summary>
    /// Gets a list of pairs containing widget IDs and names.
    /// </summary>
    public List<(string id, string name)> WidgetsIdNamesPairs
    {
        get
        {
            if (_database == null)
            {
                throw new NullReferenceException();
            }
            List<(string, string)> res = new(_database.Count);
            _database.Data.ForEach(widget => res.Add((widget.Id, widget.Name)));
            return res;
        }
    }

    /// <summary>
    /// Gets the number of fields in the JSON representation of a widget.
    /// </summary>
    public int FieldsCount
    {
        get
        {
            if (_database == null)
            {
                throw new NullReferenceException();
            }
            return Database.FieldsCount;
        }
    }

    /// <summary>
    /// Gets an array of alternative field names used in the JSON representation of a widget.
    /// </summary>
    public string[] AltenativeFieldsNames
    {
        get
        {
            if (_database == null)
            {
                throw new NullReferenceException();
            }
            return Database.JsonFieldsNames;
        }
    }

    /// <summary>
    /// Gets the original file path from which the database was loaded.
    /// </summary>
    public string OriginalFilePath =>
           _database?.OriginFilePath ?? throw new NullReferenceException();

    /// <summary>
    /// Converts the specified list of widgets to a tuple format.
    /// </summary>
    /// <param name="widgets">The list of widgets to convert.</param>
    /// <returns>A list of tuples representing widget data.</returns>
    private static List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
        List<(string specName, double specPrice, bool isCustom)> specifications)> ConvertToTuple(List<Widget> widgets)
    {
        return widgets.ConvertAll(
            item => (item.Id, item.Name, item.Quantity, item.Price, item.IsAvailable ?? false, item.ManufactureDate,
            item.Specifications.ConvertAll(spec => (spec.Name, spec.Price, spec.IsCustom ?? false))));
    }

    /// <summary>
    /// Gets the specifications by widget number.
    /// </summary>
    /// <param name="widgetNum">The number of the widget.</param>
    /// <returns>The list of specification names for the specified widget.</returns>
    public List<string> GetSpecificationsByWidgetNum(int widgetNum)
    {
        if (_database == null)
        {
            throw new NullReferenceException("Database is null");
        }
        List<string> res = new();
        _database.Data[widgetNum].Specifications.ForEach(specification => res.Add(specification.Name));
        return res;
    }

    /// <summary>
    /// Gets all items in the database.
    /// </summary>
    /// <returns>The list of tuples representing all items in the database.</returns>
    public List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
        List<(string specName, double specPrice, bool isCustom)> specifications)> GetAllItems()
    {
        if (_database == null)
        {
            throw new NullReferenceException();
        }
        return ConvertToTuple(_database.Data);
    }

    // Below are public methods to get information about specified Widget.

    public string GetIdByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].Id ?? throw new NullReferenceException();
    public string GetNameByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].Name ?? throw new NullReferenceException();
    public int GetQuantityByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].Quantity ?? throw new NullReferenceException();
    public double GetPriceByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].Price ?? throw new NullReferenceException();
    public bool GetIsAvailableByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].IsAvailable ?? throw new NullReferenceException();
    public DateTime GetManufactureDateByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].ManufactureDate ?? throw new NullReferenceException();

    // Below are public methods to get information about specified Widget.

    public int GetSpecificationsCountByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].Specifications.Count ?? throw new NullReferenceException();

    public string GetSpecificationNameByNums(int widgetNum, int specificationNum) =>
        _database?.Data[widgetNum].Specifications[specificationNum].Name ?? throw new NullReferenceException();
    public double GetSpecificationPriceByNums(int widgetNum, int specificationNum) =>
        _database?.Data[widgetNum].Specifications[specificationNum].Price ?? throw new NullReferenceException();
    public bool GetSpecificationIsCustomByNums(int widgetNum, int specifcationNum) =>
        _database?.Data[widgetNum].Specifications[specifcationNum].IsCustom ?? throw new NullReferenceException();

    // Below are public methods to change information about specified Widget.

    public void ChangeNameByWidgetNum(int widgetNum, string value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].Name = value;
    }

    public void ChangeQuantityByWidgetNum(int widgetNum, int value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].Quantity = value;
    }

    public void ChangePriceByWidgetNum(int widgetNum, double value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].Price = value;
    }

    public void ChangeIsAvailaleByWidgetNum(int widgetNum, bool value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].IsAvailable = value;
    }

    public void ChangeManufactureDateByWidgetNum(int widgetNum, DateTime value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].ManufactureDate = value;
    }
    
    // Below are public methods to change specified Specification.

    public void ChangeSpecificationNameByNums(int widgetNum, int specificationNum, string value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].Specifications[specificationNum].Name = value;
    }

    public void ChangeSpecificationIsCustomByNums(int widgetNum, int specificationNum, bool value)
    {
        if (_database == null) throw new NullReferenceException();
        _database.Data[widgetNum].Specifications[specificationNum].IsCustom = value;
    }


    private IOrderedEnumerable<Widget>? PrimarySorterByAlternativeName(string alternativeName, SortingOptions sortingOption)
    {
        if (_database == null)
        {
            throw new NullReferenceException();
        }
        IOrderedEnumerable<Widget>? orderer = null;
        switch (alternativeName)
        {
            case "widgetId":
                orderer = sortingOption == SortingOptions.Ascending ? _database.Data.OrderBy(widget => widget.Id) : _database.Data.OrderByDescending(widget => widget.Id);
                break;
            case "name":
                orderer = sortingOption == SortingOptions.Ascending ? _database.Data.OrderBy(widget => widget.Name) : _database.Data.OrderByDescending(widget => widget.Name);
                break;
            case "quantity":
                orderer = sortingOption == SortingOptions.Ascending ? _database.Data.OrderBy(widget => widget.Quantity) : _database.Data.OrderByDescending(widget => widget.Quantity);
                break;
            case "price":
                orderer = sortingOption == SortingOptions.Ascending ? _database.Data.OrderBy(widget => widget.Price) : _database.Data.OrderByDescending(widget => widget.Price);
                break;
            case "isAvailable":
                orderer = sortingOption == SortingOptions.Ascending ? _database.Data.OrderBy(widget => widget.IsAvailable) : _database.Data.OrderByDescending(widget => widget.IsAvailable);
                break;
            case "manufactureDate":
                orderer = sortingOption == SortingOptions.Ascending ? _database.Data.OrderBy(widget => widget.ManufactureDate) : _database.Data.OrderByDescending(widget => widget.ManufactureDate);
                break;
        }
        return orderer;
    }

    private static IOrderedEnumerable<Widget>? SecondarySorterByAlternativeName(IOrderedEnumerable<Widget>? orderer, string alternativeName, SortingOptions sortingOption)
    {
        switch (alternativeName)
        {
            case "widgetId":
                orderer = sortingOption == SortingOptions.Ascending ? orderer?.ThenBy(widget => widget.Id) : orderer?.ThenByDescending(widget => widget.Id);
                break;
            case "name":
                orderer = sortingOption == SortingOptions.Ascending ? orderer?.ThenBy(widget => widget.Name) : orderer?.ThenByDescending(widget => widget.Name);
                break;
            case "quantity":
                orderer = sortingOption == SortingOptions.Ascending ? orderer?.ThenBy(widget => widget.Quantity) : orderer?.ThenByDescending(widget => widget.Quantity);
                break;
            case "price":
                orderer = sortingOption == SortingOptions.Ascending ? orderer?.ThenBy(widget => widget.Price) : orderer?.ThenByDescending(widget => widget.Price);
                break;
            case "isAvailable":
                orderer = sortingOption == SortingOptions.Ascending ? orderer?.ThenBy(widget => widget.IsAvailable) : orderer?.ThenByDescending(widget => widget.IsAvailable);
                break;
            case "manufactureDate":
                orderer = sortingOption == SortingOptions.Ascending ? orderer?.ThenBy(widget => widget.ManufactureDate) : orderer?.ThenByDescending(widget => widget.ManufactureDate);
                break;
        }
        return orderer;
    }


    /// <summary>
    /// Sorts the data based on the specified keys and sorting options.
    /// </summary>
    /// <param name="keys">The list of keys for sorting.</param>
    /// <param name="sortingOptions">The dictionary containing sorting options for each key.</param>
    /// <returns>The sorted list of tuples representing the data.</returns>
    public List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
        List<(string specName, double specPrice, bool isCustom)> specifications)> Sort(List<string>? keys, Dictionary<string, SortingOptions>? sortingOptions)
    {
        if (_database == null)
            throw new NullReferenceException();
        if (keys == null)
            throw new ArgumentNullException(nameof(keys));
        if (sortingOptions == null)
            throw new ArgumentNullException(nameof(sortingOptions));
        if (keys.Count < 1)
            throw new ArgumentException("Requested prarmeters to sort should contain at least one argument", nameof(keys));

        IOrderedEnumerable<Widget>? orderer = PrimarySorterByAlternativeName(keys[0], sortingOptions[keys[0]]);

        for (int i = 1; i < keys.Count; i++)
        {
            orderer = SecondarySorterByAlternativeName(orderer, keys[i], sortingOptions[keys[i]]);
        }

        _database.AddDataToCache(orderer?.ToList() ?? _database.Data);
        return ConvertToTuple(orderer?.ToList() ?? _database.Data);
    }

    /// <summary>
    /// Saves the database to the specified file path.
    /// </summary>
    /// <param name="filePath">The file path to save the database.</param>
    public void Save(string filePath)
    {
        if (_database == null)
            throw new NullReferenceException();
        _database.Save(filePath);
    }

    /// <summary>
    /// Saves the cached data to the specified file path.
    /// </summary>
    /// <param name="filePath">The file path to save the cached data.</param>
    public void SaveCache(string filePath)
    {
        if (_database == null)
            throw new NullReferenceException();
        _database.SaveCache(filePath);
    }

    /// <summary>
    /// Gets the JSON string representation of the cached data.
    /// </summary>
    /// <returns>The JSON string representation of the cached data.</returns>
    public string GetJsonStringCache()
    {
        if (_database == null)
            throw new NullReferenceException();
        return _database.GetJsonStringCache();
    }

    /// <summary>
    /// Gets the JSON string representation of the database data.
    /// </summary>
    /// <returns>The JSON string representation of the database data.</returns>
    public string GetJsonStringData()
    {
        if (_database == null)
            throw new NullReferenceException();
        return _database.GetJsonStringData();
    }
}
