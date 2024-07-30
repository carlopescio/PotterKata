namespace Potter
{
    internal abstract class Offer(Party From, BookSet Goods, int Delta)
    {
        public override string ToString()
        {
            return From.ToString() + " offers to " + What() + " " + Goods +
                   " with a delta of " + Delta;
        }

        internal abstract void Accepted();
        internal Party From { get; } = From;
        internal BookSet Goods { get; } = Goods;
        internal int Delta { get; } = Delta;

        internal List<T> MatchingOffersByWorth<T>(List<T> offers) where T : Offer
        {
            List<T> rightProductsNegativeBalance = offers.FindAll(p => (p.Goods & Goods) == p.Goods && Delta + p.Delta < 0);
            rightProductsNegativeBalance.Sort((a, b) => a.Delta - b.Delta);
            return rightProductsNegativeBalance;
        }

        protected abstract string What();
    }
}
