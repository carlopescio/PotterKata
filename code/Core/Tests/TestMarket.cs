namespace Potter.Tests
{
    [TestClass]
    public class TestMarket
    {
        private class TrackedMerchant(BookSet Goods) : Merchant(Goods)
        {
            internal override void Add(BookSet purchasedGoods)
            {
                base.Add(purchasedGoods);
                purchased |= purchasedGoods;
            }

            internal override void Remove(BookSet soldGoods)
            {
                base.Remove(soldGoods);
                sold |= soldGoods;
            }

            internal BookSet purchased;
            internal BookSet sold;
        }

        [TestMethod]
        public void SingleSaleOf3Books()
        {
            Party.ResetId();
            List<TrackedMerchant> ms =
                [new(BookSet.II | BookSet.III),
                 new(BookSet.II),
                 new(BookSet.III),
                 new(BookSet.IV | BookSet.III | BookSet.II),
                 new(BookSet.V)];

            int priceBefore = ms.Sum(m => Pricing.DiscountedPrice(m.Goods.Variety()));

            Market mk = new();
            foreach(var m in ms)
            {
                mk.AddPurchases(m.OffersToBuy());
                mk.AddSales(m.OffersToSell());
            }

            bool doneDeal = mk.Trade();
            Assert.AreEqual( true, doneDeal);

            BookSet empty = (BookSet)0;

            Assert.AreEqual(empty, ms[0].sold);
            Assert.AreEqual(BookSet.IV, ms[0].purchased);

            Assert.AreEqual(empty, ms[1].sold);
            Assert.AreEqual(BookSet.III, ms[1].purchased);

            Assert.AreEqual(empty, ms[2].sold);
            Assert.AreEqual(BookSet.II, ms[2].purchased);

            Assert.AreEqual(BookSet.IV | BookSet.III | BookSet.II, ms[3].sold);
            Assert.AreEqual(empty, ms[3].purchased);

            Assert.AreEqual(empty, ms[4].sold);
            Assert.AreEqual(empty, ms[4].purchased);

            int priceAfter = ms.Sum(m => Pricing.DiscountedPrice(m.Goods.Variety()));
            Assert.IsTrue(priceAfter < priceBefore);
        }
    }
}
