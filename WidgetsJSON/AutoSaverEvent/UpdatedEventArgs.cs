namespace WidgetsJSON.AutoSaverEvent;

public class UpdatedEventArgs : EventArgs
{
    private readonly DateTime _happened;

    public UpdatedEventArgs() =>
        _happened = DateTime.Now;

    public UpdatedEventArgs(DateTime happened) =>
        _happened = happened;

    public DateTime Happened => _happened;
}
