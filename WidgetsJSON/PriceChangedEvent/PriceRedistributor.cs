using AdvancedTools;

namespace WidgetsJSON.PriceChangedEvent;

/// <summary>
/// Class responsible for redistributing price changes among widget specifications.
/// </summary>
public class PriceRedistributor
{
    private static readonly Random s_random = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="PriceRedistributor"/> class.
    /// </summary>
    public PriceRedistributor() { }

    /// <summary>
    /// Event handler for the PriceChanged event.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments containing the price difference.</param>
    private void OnPriceChangedEventHandler(object? sender, PriceChangedEventArgs e)
    {
        var widget = sender as Widget ?? throw new ArgumentException("Sender should be a Widget and not null");
        if (!TryRedistributePrice(e.Difference, widget))
        {
            throw new ArgumentException("Cannot redistribute price for the given widget", nameof(sender));
        }

    }

    /// <summary>
    /// Tries to redistribute the given price difference among widget specifications.
    /// </summary>
    /// <param name="difference">The difference in price.</param>
    /// <param name="widget">The widget whose specifications need redistribution.</param>
    /// <returns>True if successful, false otherwise.</returns>
    private static bool TryRedistributePrice(double difference, Widget widget)
    {
        List<Specification> shuffledSpecifications = new(widget.Specifications);
        s_random.Shuffle(shuffledSpecifications);

        foreach (var specification in shuffledSpecifications)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(widget), "Specifications of sender cannot be null");
            }

            if (specification.Price + difference >= 0)
            {
                specification.Price += difference;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Registers a widget for price redistribution.
    /// </summary>
    /// <param name="obj">The widget to register.</param>
    public void Register(Widget obj) =>
        obj.PriceChanged += OnPriceChangedEventHandler;


    /// <summary>
    /// Unregisters a widget from price redistribution.
    /// </summary>
    /// <param name="obj">The widget to unregister.</param>
    public void Unregister(Widget obj) =>
        obj.PriceChanged -= OnPriceChangedEventHandler;
}
