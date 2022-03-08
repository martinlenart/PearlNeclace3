namespace PearlNecklace
{
    public interface INecklaceBag
    {
        public INecklace this[int idx] { get; }
        public decimal Price { get; }

        public IPearl MostExpensivePearl { get; }

        int Count();
        int CountPearls(PearlColor? color);

    }
}
