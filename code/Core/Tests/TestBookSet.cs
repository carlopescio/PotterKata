namespace Potter.Tests
{
    [TestClass]
    public class TestBookSet
    {
        [TestMethod]
        public void Variety0()
        {
            BookSet b = default;
            int n = b.Variety();
            Assert.AreEqual(0, n);
        }


        [TestMethod]
        public void Variety3()
        {
            BookSet b = BookSet.I | BookSet.IV | BookSet.V;
            int n = b.Variety();
            Assert.AreEqual(3, n);
        }


        [TestMethod]
        public void Missing()
        {
            BookSet b = BookSet.I | BookSet.IV | BookSet.V;
            var missing = b.Missing().ToList();
            List<BookSet> expected = [BookSet.II, BookSet.III];
            CollectionAssert.AreEqual(expected, missing);
        }

        [TestMethod]
        public void ByOne()
        {
            BookSet purchase = BookSet.I | BookSet.IV | BookSet.V;
            var by1 = purchase.ByOne().ToList();
            List<BookSet> expected = [BookSet.I, BookSet.IV, BookSet.V];
            CollectionAssert.AreEqual(expected, by1);
        }


        [TestMethod]
        public void ByTwo()
        {
            BookSet purchase = BookSet.I | BookSet.IV | BookSet.V;
            var by2 = purchase.ByTwo().ToList();
            List<BookSet> expected = [BookSet.I | BookSet.IV, BookSet.I | BookSet.V, BookSet.IV | BookSet.V];
            CollectionAssert.AreEqual(expected, by2);
        }

        [TestMethod]
        public void ByTwoAndThree()
        {
            BookSet v5 = BookSet.I | BookSet.II | BookSet.III | BookSet.IV | BookSet.V;
            var v5by2 = v5.ByTwo().ToList();
            List<BookSet> expected5by2 =
                [BookSet.I | BookSet.II, BookSet.I | BookSet.III, BookSet.I | BookSet.IV, BookSet.I | BookSet.V,
                 BookSet.II | BookSet.III, BookSet.II | BookSet.IV, BookSet.II | BookSet.V,
                 BookSet.III | BookSet.IV, BookSet.III | BookSet.V,
                 BookSet.IV | BookSet.V];
            CollectionAssert.AreEqual(expected5by2, v5by2);

            var v5by3 = v5.ByThree().ToList();
            List<BookSet> expected5by3 =
                [BookSet.I | BookSet.II | BookSet.III, BookSet.I | BookSet.II | BookSet.IV, BookSet.I | BookSet.II | BookSet.V,
                 BookSet.I | BookSet.III | BookSet.IV, BookSet.I | BookSet.III | BookSet.V,
                 BookSet.I | BookSet.IV | BookSet.V,
                 BookSet.II | BookSet.III | BookSet.IV, BookSet.II | BookSet.III | BookSet.V,
                 BookSet.II | BookSet.IV | BookSet.V,
                 BookSet.III | BookSet.IV | BookSet.V];
            CollectionAssert.AreEqual(expected5by3, v5by3);

            BookSet v4 = BookSet.II | BookSet.III | BookSet.IV | BookSet.V;
            var v4by2 = v4.ByTwo().ToList();
            List<BookSet> expected4by2 =
                 [BookSet.II | BookSet.III, BookSet.II | BookSet.IV, BookSet.II | BookSet.V,
                 BookSet.III | BookSet.IV, BookSet.III | BookSet.V,
                 BookSet.IV | BookSet.V];
            CollectionAssert.AreEqual(expected4by2, v4by2);

            var v4by3 = v4.ByThree().ToList();
            List<BookSet> expected4by3 =
                [BookSet.II | BookSet.III | BookSet.IV, BookSet.II | BookSet.III | BookSet.V,
                 BookSet.II | BookSet.IV | BookSet.V,
                 BookSet.III | BookSet.IV | BookSet.V];
            CollectionAssert.AreEqual(expected4by3, v4by3);
        }
    }
}