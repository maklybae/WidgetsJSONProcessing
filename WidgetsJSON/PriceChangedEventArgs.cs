namespace WidgetsJSON
{
    public class PriceChangedEventArgs : EventArgs
    {
        private readonly double _difference;

        private PriceChangedEventArgs() => _difference = 0;

        public PriceChangedEventArgs(int difference) =>
            _difference = difference;

        public double Difference => _difference;
    }
}