namespace Potter
{
    internal class OfferToBuy(Party From, BookSet Goods, int priceIncrease) : Offer(From, Goods, priceIncrease)
    {
        internal override void Accepted()
        {
            From.Add(Goods);
        }

        protected override string What()
        {
            return "buy";
        }
    }
}
