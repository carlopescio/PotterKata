namespace Potter
{
    public class Basket
    {
        public void Add(BookSet b)
        {
            dealers.Add(new Merchant(b));
        }

        public IEnumerable<(BookSet books, Decimal price)> OptimalPartition()
        {
            bool done = false;
            while(!done)
            {
                Market mk = new();
                foreach(var m in dealers)
                {
                    mk.AddPurchases(m.OffersToBuy());
                    mk.AddSales(m.OffersToSell());
                }

                done = ! mk.Trade();

                dealers.RemoveAll(m => m.HasEmptyStock());
            }
            IEnumerable<BookSet> sets = dealers.Select(d => d.Goods);
            return sets.Select(s => (s, Pricing.ToNominalPrice(Pricing.DiscountedPrice(s.Variety()))));
        }

        private List<Merchant> dealers = [];
    }
}
