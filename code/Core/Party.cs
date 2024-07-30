
namespace Potter
{
    internal abstract class Party()
    {
        internal int Id { get; } = Next();

        internal abstract void Add(BookSet newGoods);

        internal abstract void Remove(BookSet goods);

        internal static void ResetId()
        {
            current = 0;
        }

        private static int current;
        private static int Next()
        {
            return current++;
        }
    }
}
