namespace Model;

public class RequestProcessor
{
    private Database _database;
    
    private RequestProcessor() =>
        _database = new();

    public RequestProcessor(string filePath) =>
        _database = new Database(filePath);

    public void RebaseProcessor(string filePath) =>
        _database = new Database(filePath);

    public void ChangeWidgetField(int widgetNum, string fieldName, object? replacingVlaue)
    {
        if (replacingVlaue == null)
        {
            throw new ArgumentNullException(nameof(replacingVlaue));
        }

        switch(fieldName)
        {
            case "widgetId":
                throw new ArgumentException("Cannot change this field (it is unique)", nameof(fieldName));
            case "name" when replacingVlaue is string v:
                _database.Data[widgetNum].Name = v;
                break;
            case "quantity" when replacingVlaue is int v:
                _database.Data[widgetNum].Quantity = v;
                break;
            case "price" when replacingVlaue is double v:
                _database.Data[widgetNum].Price = v;
                break;
            case "isAvailable" when replacingVlaue is bool v:
                _database.Data[widgetNum].IsAvailable = v;
                break;
            case "manufactureDate" when replacingVlaue is DateTime v:
                _database.Data[widgetNum].ManufactureDate = v;
                break;
            default:
                throw new ArgumentException("Cannot change the given field by this replacing value", nameof(replacingVlaue));
        }
    }

    public void ChangeSpecificationField(int widgetNum, int specificationNum, string fieldName, object? replacingVlaue)
    {
        if (replacingVlaue == null)
        {
            throw new ArgumentNullException(nameof(replacingVlaue));
        }

        switch (fieldName) { 
            case "specName" when replacingVlaue is string v:
                _database.Data[widgetNum].Specifications[specificationNum].Name = v;
                break;
            case "specPrice" when replacingVlaue is double:
                throw new ArgumentException("Cannot change specification price, change widget price instead", nameof(fieldName));
            case "isCustom" when replacingVlaue is bool v:
                _database.Data[widgetNum].Specifications[specificationNum].IsCustom = v;
                break;
            default:
                throw new ArgumentException("Cannot change the given field by this replacing value", nameof(replacingVlaue));
        }
    }
}
