namespace Potter.Tests
{
    [TestClass]
    public class TestBasket_Standard
    {
        [TestMethod]
        public void Collapse5To1()
        {
            Basket b = new();
            b.Add(BookSet.I);
            b.Add(BookSet.II);
            b.Add(BookSet.III);
            b.Add(BookSet.IV);
            b.Add(BookSet.V);
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected = [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M)];
            CollectionAssert.AreEquivalent(expected, best);
        }

        [TestMethod]
        public void Unoptimizable()
        {
            Basket b = new();
            b.Add(BookSet.I);
            b.Add(BookSet.I);
            b.Add(BookSet.I);
            b.Add(BookSet.I);
            b.Add(BookSet.I);
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected =
                [(BookSet.I, 8.0M), (BookSet.I, 8.0M), (BookSet.I, 8.0M), (BookSet.I, 8.0M), (BookSet.I, 8.0M)];
            CollectionAssert.AreEquivalent(expected, best);
        }

        [TestMethod]
        public void FromTheBottom_4_4()
        {
            Basket b = new();
            b.Add(BookSet.I);
            b.Add(BookSet.I);
            b.Add(BookSet.II);
            b.Add(BookSet.II);
            b.Add(BookSet.III);
            b.Add(BookSet.III);
            b.Add(BookSet.IV);
            b.Add(BookSet.V);
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected =
                [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV, 25.6M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.V, 25.6M)];
            CollectionAssert.AreEquivalent(expected, best);
        }

        [TestMethod]
        public void Arrange23In3_5_2_4()
        {
            Basket b = new();
            for( int i = 0; i < 5; ++i)
            {
                b.Add(BookSet.I);
                b.Add(BookSet.II);
                b.Add(BookSet.IV);
            }
            for(int i = 0; i < 4; ++i)
            {
                b.Add(BookSet.III);
                b.Add(BookSet.V);
            }
            List<(BookSet books, Decimal price)> best = b.OptimalPartition().ToList();
            List<(BookSet books, Decimal price)> expected = 
                [(BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V, 30.0M),
                 (BookSet.I | BookSet.II | BookSet.III | BookSet.IV, 25.6M),
                 (BookSet.I | BookSet.II | BookSet.IV | BookSet.V, 25.6M)
                ];
            CollectionAssert.AreEquivalent(expected, best);
        }
    }
}
