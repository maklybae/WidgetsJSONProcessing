using System.Text.Json;
using WidgetsJSON;
namespace Model;

public class Database
{
    private readonly List<Widget> _data;
    private readonly string _originFilePath;
    private readonly AutoSaver? _autoSaver;
    private readonly PriceRedistributor? _priceRedistributor;
   
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

        _autoSaver = new(jsonString);
        _priceRedistributor = new();

        RegisterSubscribers();
    }

    public List<Widget> Data => _data;

    public string OriginFilePath => _originFilePath;

    public string OriginFileName => Path.GetFileName(OriginFileName);   
    
    private void RegisterSubscribers()
    {
        foreach (var widget in Data)
        {
            _autoSaver?.Register(widget);
            widget.Specifications.ForEach(spec => _autoSaver?.Register(spec));
            _priceRedistributor?.Register(widget);
        }
    }

    public void Save() { }

    public void Save(string filePath) { }
}