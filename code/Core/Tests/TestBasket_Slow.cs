namespace Potter.Tests
{
    [TestClass]
    public class TestBasket_Slow
    {
        // [TestMethod]
        public void Collapse110To30()
        {
            Basket b = new();
            for(int i = 0; i < 20; ++i)
                b.Add(BookSet.I);
            for(int i = 0; i < 20; ++i)
                b.Add(BookSet.II);
            for(int i = 0; i < 20; ++i)
                b.Add(BookSet.III);
            for(int i = 0; i < 20; ++i)
                b.Add(BookSet.IV);
            for(int i = 0; i < 30; ++i)
                b.Add(BookSet.V);
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            Assert.AreEqual(30, best.Count);
        }

        // [TestMethod]
        public void Collapse210To50()
        {
            Basket b = new();
            for(int i = 0; i < 40; ++i)
                b.Add(BookSet.I);
            for(int i = 0; i < 40; ++i)
                b.Add(BookSet.II);
            for(int i = 0; i < 40; ++i)
                b.Add(BookSet.III);
            for(int i = 0; i < 40; ++i)
                b.Add(BookSet.IV);
            for(int i = 0; i < 50; ++i)
                b.Add(BookSet.V);
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            Assert.AreEqual(50, best.Count);
        }

        // [TestMethod]
        public void Collapse600To200()
        {
            Basket b = new();
            for(int i = 0; i < 60; ++i)
                b.Add(BookSet.I);
            for(int i = 0; i < 60; ++i)
                b.Add(BookSet.II);
            for(int i = 0; i < 60; ++i)
                b.Add(BookSet.III);
            for(int i = 0; i < 60; ++i)
                b.Add(BookSet.IV);
            for(int i = 0; i < 100; ++i)
                b.Add(BookSet.V);
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            Assert.AreEqual(100, best.Count);
        }
    }
}
