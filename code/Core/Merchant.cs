namespace Potter
{
    internal class Merchant(BookSet Goods) : Party
    {
        public override string ToString()
        {
            var price = Pricing.DiscountedPrice(Goods.Variety());
            return "Merchant " + Id + " owns " + Goods.ToString() +
                   " worth " + price;
        }

        internal override void Add(BookSet purchasedGoods)
        {
            Goods |= purchasedGoods;
        }

        internal override void Remove(BookSet soldGoods)
        {
            Goods ^= soldGoods;
        }

        internal IEnumerable<OfferToSell> OffersToSell()
        {
            int variety = Goods.Variety();
            int currentPrice = Pricing.DiscountedPrice(variety);

            int priceSelling1 = Pricing.DiscountedPrice(variety - 1);
            var res = Goods.ByOne().Select(g => new OfferToSell(this, g, priceSelling1 - currentPrice));

            int priceSelling2 = Pricing.DiscountedPrice(variety - 2);
            res = res.Concat(Goods.ByTwo().Select(g => new OfferToSell(this, g, priceSelling2 - currentPrice)));

            int priceSelling3 = Pricing.DiscountedPrice(variety - 3);
            res = res.Concat(Goods.ByThree().Select(g => new OfferToSell(this, g, priceSelling3 - currentPrice)));

            return res;
        }

        internal IEnumerable<OfferToBuy> OffersToBuy()
        {
            int variety = Goods.Variety();
            int currentPrice = Pricing.DiscountedPrice(variety);
            int priceBuying1 = Pricing.DiscountedPrice(variety + 1);
            return Goods.Missing().Select(g => new OfferToBuy(this, g, priceBuying1 - currentPrice));
        }

        internal bool HasEmptyStock()
        {
            return Goods.Variety() == 0;
        }

        internal BookSet Goods { get; private set; } = Goods;
    }
}
