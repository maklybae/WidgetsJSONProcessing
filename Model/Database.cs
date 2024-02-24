using System.Text.Json;
using WidgetsJSON;
namespace Model;

public class Database
{
    public const int FieldsCount = 7;
    private static readonly string[] s_jsonFieldsNames = new string[]
    {
        "widgetId",
        "name",
        "quantity",
        "price",
        "isAvailable",
        "manufactureDate",
        "specifications"
    };

    private readonly List<Widget> _data;
    private readonly string _originFilePath;
    private readonly AutoSaver? _autoSaver;
    private readonly PriceRedistributor? _priceRedistributor;
    private readonly JsonSerializerOptions _serializerOptions = new() { WriteIndented = true };

    private List<Widget>? _cacheData;
    // jsonString = JsonSerializer.Serialize(widgets, options);

    public Database()
    {
        _data = new();
        _originFilePath = string.Empty;
        _autoSaver = default;
        _priceRedistributor = default;
    }

    public Database(string filePath)
    {
        _originFilePath = filePath;

        string jsonString = File.ReadAllText(filePath);
        _data = JsonSerializer.Deserialize<List<Widget>>(jsonString) ??
            throw new ArgumentException("File is empty or consist of wrong data");

        _autoSaver = new(filePath, TrySave);
        _priceRedistributor = new();

        RegisterSubscribers();
    }

    public static string[] JsonFieldsNames => s_jsonFieldsNames;

    public List<Widget> Data => _data;

    public string OriginFilePath => _originFilePath;

    public string OriginFileName => Path.GetFileName(OriginFileName);

    public int Count => Data.Count;
    
    private void RegisterSubscribers()
    {
        foreach (var widget in Data)
        {
            _autoSaver?.Register(widget);
            widget.Specifications.ForEach(spec => _autoSaver?.Register(spec));
            _priceRedistributor?.Register(widget);
        }
    }

    public void AddDataToCache(List<Widget> data)
    {
        _cacheData = data;
    }

    public void TrySave(string path)
    {
        // Assume that auto saving can be failed, however user is not able to fix it.
        try
        {
            File.WriteAllText(path, GetJsonStringData());
        }
        catch (Exception) { }
    }


    public void Save(string filePath) =>
        File.WriteAllText(filePath, GetJsonStringData());

    public void SaveCache(string filePath) =>
        File.WriteAllText(filePath, GetJsonStringCache());

    public string GetJsonStringData() =>
        JsonSerializer.Serialize(Data, _serializerOptions);

    public string GetJsonStringCache() =>
        JsonSerializer.Serialize(_cacheData, _serializerOptions);

}