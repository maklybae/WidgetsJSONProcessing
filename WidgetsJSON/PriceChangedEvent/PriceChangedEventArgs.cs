namespace WidgetsJSON.PriceChangedEvent
{
    public class PriceChangedEventArgs : EventArgs
    {
        private readonly double _difference;

        private PriceChangedEventArgs() => _difference = 0;

        public PriceChangedEventArgs(double difference) =>
            _difference = difference;

        public double Difference => _difference;
    }
}