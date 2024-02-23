using WidgetsJSON;

namespace Model;

public class RequestProcessor
{
    private Database? _database;

    public RequestProcessor() { }

    public RequestProcessor(string filePath) =>
        _database = new Database(filePath);

    public bool IsReady => _database != null;

    public void RebaseProcessor(string filePath) =>
        _database = new Database(filePath);


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

    private static List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
        List<(string specName, double specPrice, bool isCustom)> specifications)> ConvertToTuple(List<Widget> widgets)
    {
        return widgets.ConvertAll(
            item => (item.Id, item.Name, item.Quantity, item.Price, item.IsAvailable ?? false, item.ManufactureDate,
            item.Specifications.ConvertAll(spec => (spec.Name, spec.Price, spec.IsCustom ?? false))));
    }

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

    public List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
        List<(string specName, double specPrice, bool isCustom)> specifications)> GetAllItems()
    {
        if (_database == null)
        {
            throw new NullReferenceException();
        }
        return ConvertToTuple(_database.Data);
    }

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
    public int GetSpecificationsCountByWidgetNum(int widgetNum) =>
        _database?.Data[widgetNum].Specifications.Count ?? throw new NullReferenceException();

    public string GetSpecificationNameByNums(int widgetNum, int specificationNum) =>
        _database?.Data[widgetNum].Specifications[specificationNum].Name ?? throw new NullReferenceException();
    public double GetSpecificationPriceByNums(int widgetNum, int specificationNum) =>
        _database?.Data[widgetNum].Specifications[specificationNum].Price ?? throw new NullReferenceException();
    public bool GetSpecificationIsCustomByNums(int widgetNum, int specifcationNum) =>
        _database?.Data[widgetNum].Specifications[specifcationNum].IsCustom ?? throw new NullReferenceException();

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

    private IOrderedEnumerable<Widget>? SecondarySorterByAlternativeName(IOrderedEnumerable<Widget>? orderer, string alternativeName, SortingOptions sortingOption)
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

    // Обработать исключение
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

        return ConvertToTuple(orderer?.ToList() ?? _database.Data);
    }
}
