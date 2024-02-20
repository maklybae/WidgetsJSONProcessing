using AdvancedTools;

namespace WidgetsJSON
{
    public class PriceRedistributor
    {
        private static readonly Random s_random = new();

        private void OnPriceChangedEventHandler(object? sender, PriceChangedEventArgs e)
        {
            var widget = sender as Widget ?? throw new ArgumentException("Sender should be a Widget and not null");
            if (!TryRedistributePrice(e.Difference, widget))
            {
                throw new ArgumentException("Cannot redistribute price for the given widget", nameof(sender));
            }

        }

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

        public void Register(Widget obj) =>
            obj.PriceChanged += OnPriceChangedEventHandler;

        public void Unregister(Widget obj) =>
            obj.PriceChanged -= OnPriceChangedEventHandler;
    }
}
