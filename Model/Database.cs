using System.Text.Json;
using WidgetsJSON;
using WidgetsJSON.AutoSaverEvent;
using WidgetsJSON.PriceChangedEvent;

namespace Model;

/// <summary>
/// Represents a database containing a list of widgets and provides methods for saving and managing data.
/// </summary>
internal class Database
{
    /// <summary>
    /// The number of fields in the JSON representation of a widget.
    /// </summary>
    public const int FieldsCount = 7;

    // Field names in the JSON representation of a widget.
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

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class with empty data.
    /// </summary>
    public Database()
    {
        _data = new();
        _originFilePath = string.Empty;
        _autoSaver = default;
        _priceRedistributor = default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class with data loaded from a JSON file.
    /// </summary>
    /// <param name="filePath">The path to the JSON file.</param>
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

    /// <summary>
    /// Gets the names of fields in the JSON representation of a widget.
    /// </summary>
    public static string[] JsonFieldsNames => s_jsonFieldsNames;

    /// <summary>
    /// Gets the list of widgets in the database.
    /// </summary>
    public List<Widget> Data => _data;

    /// <summary>
    /// Gets the path of the original JSON file.
    /// </summary>
    public string OriginFilePath => _originFilePath;

    /// <summary>
    /// Gets the name of the original JSON file.
    /// </summary>
    public string OriginFileName => Path.GetFileName(OriginFileName);

    /// <summary>
    /// Gets the number of widgets in the database.
    /// </summary>
    public int Count => Data.Count;

    /// <summary>
    /// Register new objects for evetns Updated & PriceChanged.
    /// </summary>
    /// <param name="data">The data to regeister.</param>
    private void RegisterSubscribers()
    {
        foreach (var widget in Data)
        {
            _autoSaver?.Register(widget);
            widget.Specifications.ForEach(spec => _autoSaver?.Register(spec));
            _priceRedistributor?.Register(widget);
        }
    }

    /// <summary>
    /// Adds data to the cache for saving.
    /// </summary>
    /// <param name="data">The data to be added to the cache.</param>
    public void AddDataToCache(List<Widget> data)
    {
        _cacheData = data;
    }

    /// <summary>
    /// Tries to save the data to the specified path.
    /// </summary>
    /// <param name="path">The path to save the data.</param>
    public void TrySave(string path)
    {
        // Assume that auto-saving can be failed, however user is not able to fix it.
        try
        {
            File.WriteAllText(path, GetJsonStringData());
        }
        catch (Exception) { }
    }

    /// <summary>
    /// Saves the data to the specified file.
    /// </summary>
    /// <param name="filePath">The path to save the data.</param>
    public void Save(string filePath) =>
        File.WriteAllText(filePath, GetJsonStringData());

    /// <summary>
    /// Saves the cache data to the specified file.
    /// </summary>
    /// <param name="filePath">The path to save the cache data.</param>
    public void SaveCache(string filePath) =>
        File.WriteAllText(filePath, GetJsonStringCache());

    /// <summary>
    /// Gets the JSON string representation of the database data.
    /// </summary>
    /// <returns>The JSON string representation of the database data.</returns>
    public string GetJsonStringData() =>
        JsonSerializer.Serialize(Data, _serializerOptions);

    /// <summary>
    /// Gets the JSON string representation of the cache data.
    /// </summary>
    /// <returns>The JSON string representation of the cache data.</returns>
    public string GetJsonStringCache() =>
        JsonSerializer.Serialize(_cacheData, _serializerOptions);

}