namespace WidgetsJSON.PriceChangedEvent;

/// <summary>
/// Provides event arguments for the price changed event.
/// </summary>
public class PriceChangedEventArgs : EventArgs
{
    private readonly double _difference;

    private PriceChangedEventArgs() => _difference = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="PriceChangedEventArgs"/> class with a specified difference.
    /// </summary>
    /// <param name="difference">The difference in price.</param>
    public PriceChangedEventArgs(double difference) =>
        _difference = difference;


    /// <summary>
    /// Gets the difference in price.
    /// </summary>
    public double Difference => _difference;
}