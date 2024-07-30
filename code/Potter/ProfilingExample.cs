namespace Potter
{
    public static class ProfilingExample
    {
        public static void Main()
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
    }
}
