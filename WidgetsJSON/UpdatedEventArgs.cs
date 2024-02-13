namespace WidgetsJSON
{
    public class UpdatedEventArgs : EventArgs
    {
        private readonly DateTime _happened;

        public UpdatedEventArgs(DateTime happened) =>
            _happened = happened;

        public DateTime Happened => _happened;
    }
}
