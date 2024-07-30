namespace Potter.Tests
{
    [TestClass]
    public class TestBasket_Redistribute
    {
        [TestMethod]
        public void Switch53To44()
        {
            Basket b = new();
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V);
            b.Add(BookSet.I | BookSet.II | BookSet.III);

            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected =
                [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV, 25.6M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.V, 25.6M)];
            CollectionAssert.AreEquivalent(expected, best);
        }

        [TestMethod]
        public void Switch444To552()
        {
            Basket b = new();
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.IV);
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.V);
            b.Add(BookSet.I | BookSet.II | BookSet.IV | BookSet.V);

            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected =
                [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II, 15.2M)];
            CollectionAssert.AreEquivalent(expected, best);
        }

        [TestMethod]
        public void Switch4444To5551()
        {
            Basket b = new();
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.IV);
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.V);
            b.Add(BookSet.I | BookSet.II | BookSet.IV | BookSet.V);
            b.Add(BookSet.I | BookSet.III | BookSet.IV | BookSet.V);

            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected =
                [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I, 8M)];
            CollectionAssert.AreEquivalent(expected, best);
        }

        [TestMethod]
        public void Switch44444To5555()
        {
            Basket b = new();
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.IV);
            b.Add(BookSet.I | BookSet.II | BookSet.III | BookSet.V);
            b.Add(BookSet.I | BookSet.II | BookSet.IV | BookSet.V);
            b.Add(BookSet.I | BookSet.III | BookSet.IV | BookSet.V);
            b.Add(BookSet.II | BookSet.III | BookSet.IV | BookSet.V);

            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected =
                [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M)];
            CollectionAssert.AreEquivalent(expected, best);
        }
    }
}
