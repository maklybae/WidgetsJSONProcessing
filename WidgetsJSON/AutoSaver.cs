namespace WidgetsJSON
{
    public class AutoSaver
    {
        private const int MaxDelayInSeconds = 15;

        private string _originalJsonFileName = "tmp.json";
        private DateTime _previusHappened = DateTime.MinValue;

        private AutoSaver() { }

        public AutoSaver(string originalJsonFileName)
        {
            OriginalJsonFileName = originalJsonFileName;
        }

        public string OriginalJsonFileName { get { return _originalJsonFileName; } set { _originalJsonFileName = value; } }

        private void OnUpdatedEventHandler(object? sender, UpdatedEventArgs e)
        { 
            if ((e.Happened - _previusHappened).TotalSeconds <= MaxDelayInSeconds)
            {
                Save();
            }

            _previusHappened = e.Happened;
        }

        private void Save() { }

        public void Register(JSONDataType obj) =>
            obj.Updated += OnUpdatedEventHandler;

        public void Unregister(JSONDataType obj) =>
            obj.Updated -= OnUpdatedEventHandler;
    }
}
