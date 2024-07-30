namespace Potter
{
    internal class OfferToSell(Party From, BookSet Goods, int priceDecrease) : Offer(From, Goods, priceDecrease)
    {
        internal override void Accepted()
        {
            From.Remove(Goods);
        }

        protected override string What()
        {
            return "sell";
        }
    }
}
