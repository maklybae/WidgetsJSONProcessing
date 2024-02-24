namespace WidgetsJSON.AutoSaverEvent
{
    public class AutoSaver
    {
        private const int MaxDelayInSeconds = 15;

        private readonly string _pathToAutoSave = "tmp.json";
        private readonly Action<string>? _saveAction;
        private DateTime _previusHappened = DateTime.MinValue;

        private AutoSaver() { }

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
                File.AppendAllText("events.log", $"{DateTime.Now} AutoSaver{Environment.NewLine}");
            }

            _previusHappened = e.Happened;
        }

        private void Save() => _saveAction?.Invoke(_pathToAutoSave);

        public void Register(JSONDataType obj) =>
            obj.Updated += OnUpdatedEventHandler;

        public void Unregister(JSONDataType obj) =>
            obj.Updated -= OnUpdatedEventHandler;
    }
}
