namespace WidgetsJSON.AutoSaverEvent;

/// <summary>
/// Handles auto-saving functionality for objects that implement the <see cref="JSONDataType"/> interface.
/// </summary>
public class AutoSaver
{
    private const int MaxDelayInSeconds = 15;

    private readonly string _pathToAutoSave = "tmp.json";
    private readonly Action<string>? _saveAction;
    private DateTime _previusHappened = DateTime.MinValue;

    private AutoSaver() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoSaver"/> class with the original file path and a save action.
    /// </summary>
    /// <param name="originalPath">The original file path.</param>
    /// <param name="saveAction">The action to save the file.</param>
    public AutoSaver(string originalPath, Action<string> saveAction)
    {
        _saveAction = saveAction;
        FileInfo jsonFile = new(originalPath);
        _pathToAutoSave = Path.Combine(
            jsonFile.DirectoryName ?? string.Empty, $"{Path.GetFileNameWithoutExtension(jsonFile.Name)}_tmp.json");
    }

    private void OnUpdatedEventHandler(object? sender, UpdatedEventArgs e)
    {
        if ((e.Happened - _previusHappened).TotalSeconds <= MaxDelayInSeconds)
        {
            Save();
        }

        _previusHappened = e.Happened;
    }

    private void Save() => _saveAction?.Invoke(_pathToAutoSave);

    /// <summary>
    /// Registers an object for auto-saving by subscribing to its <see cref="JSONDataType.Updated"/> event.
    /// </summary>
    /// <param name="obj">The object implementing the <see cref="JSONDataType"/> interface.</param>
    public void Register(JSONDataType obj) =>
        obj.Updated += OnUpdatedEventHandler;

    /// <summary>
    /// Unregisters an object from auto-saving by unsubscribing from its <see cref="JSONDataType.Updated"/> event.
    /// </summary>
    /// <param name="obj">The object implementing the <see cref="JSONDataType"/> interface.</param>S
    public void Unregister(JSONDataType obj) =>
        obj.Updated -= OnUpdatedEventHandler;
}
