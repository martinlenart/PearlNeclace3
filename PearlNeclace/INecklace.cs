namespace PearlNecklace
{
    public interface INecklace
    {
        public IPearl this[int idx] { get; }
        public decimal Price { get; }
        public IPearl MostExpensivePearl { get; }

        int Count();
        int Count(PearlType type);
        int Count(PearlColor? color);
        void Sort();
        public int IndexOf(IPearl pearl);
        public void ForEachPearl(Action<IPearl> pearlAction);
    }
}
