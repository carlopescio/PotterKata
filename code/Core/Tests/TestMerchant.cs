namespace Potter.Tests
{
    [TestClass]
    public class TestMerchant
    {
        [TestMethod]
        public void AddRemoveBookSet()
        {
            Party.ResetId();

            Merchant m = new(BookSet.I | BookSet.IV | BookSet.V);
            string p = m.ToString();
            Assert.AreEqual("Merchant 0 owns I, IV, V worth 270", p);

            BookSet sale = BookSet.I | BookSet.V;
            m.Remove(sale);
            string s = m.ToString();
            Assert.AreEqual("Merchant 0 owns IV worth 100", s);
        }

        [TestMethod]
        public void OffersGeneratedBy3()
        {
            Party.ResetId();

            Merchant m = new(BookSet.I | BookSet.IV | BookSet.V);

            var toSell = m.OffersToSell().Select(s => s.ToString()).ToList();
            List<String> expectedSales =
                ["Merchant 0 owns I, IV, V worth 270 offers to sell I with a delta of -80",
                 "Merchant 0 owns I, IV, V worth 270 offers to sell IV with a delta of -80",
                 "Merchant 0 owns I, IV, V worth 270 offers to sell V with a delta of -80",
                 "Merchant 0 owns I, IV, V worth 270 offers to sell I, IV with a delta of -170",
                 "Merchant 0 owns I, IV, V worth 270 offers to sell I, V with a delta of -170",
                 "Merchant 0 owns I, IV, V worth 270 offers to sell IV, V with a delta of -170",
                 "Merchant 0 owns I, IV, V worth 270 offers to sell I, IV, V with a delta of -270"];
            CollectionAssert.AreEqual(expectedSales, toSell);

            var toBuy = m.OffersToBuy().Select(s => s.ToString()).ToList();
            List<String> expectedPurchases =
                ["Merchant 0 owns I, IV, V worth 270 offers to buy II with a delta of 50",
                 "Merchant 0 owns I, IV, V worth 270 offers to buy III with a delta of 50"];
            CollectionAssert.AreEqual(expectedPurchases, toBuy);
        }

        [TestMethod]
        public void OffersGeneratedBy1()
        {
            Party.ResetId();

            Merchant m = new(BookSet.I);

            var toSell = m.OffersToSell().Select(s => s.ToString()).ToList();
            List<String> expectedSales =
                ["Merchant 0 owns I worth 100 offers to sell I with a delta of -100"];
            CollectionAssert.AreEqual(expectedSales, toSell);

            var toBuy = m.OffersToBuy().Select(s => s.ToString()).ToList();
            List<String> expectedPurchases =
                ["Merchant 0 owns I worth 100 offers to buy II with a delta of 90",
                 "Merchant 0 owns I worth 100 offers to buy III with a delta of 90",
                 "Merchant 0 owns I worth 100 offers to buy IV with a delta of 90",
                 "Merchant 0 owns I worth 100 offers to buy V with a delta of 90"];
            CollectionAssert.AreEqual(expectedPurchases, toBuy);
        }



        [TestMethod]
        public void OffersAccepted()
        {
            Party.ResetId();
            Merchant m = new(BookSet.I);

            BookSet b = BookSet.IV | BookSet.V;
            OfferToBuy otb = new(m, b, 0);
            otb.Accepted();
            string sb = m.ToString();
            Assert.AreEqual("Merchant 0 owns I, IV, V worth 270", sb);

            BookSet s = BookSet.I | BookSet.V;
            OfferToSell ots = new(m, s, 0);
            ots.Accepted();
            string ss = m.ToString();
            Assert.AreEqual("Merchant 0 owns IV worth 100", ss);
        }
    }
}