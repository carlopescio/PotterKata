using System.Numerics;

namespace Potter
{
    [Flags] public enum BookSet { I = 1, II = 2, III = 4, IV = 8, V = 16 }

    internal static class VolumesExtension
    {
        public static IEnumerable<BookSet> Missing(this BookSet self)
        {
            foreach(BookSet v in Enum.GetValues(typeof(BookSet)))
            {
                if((self & v) != v)
                    yield return v;
            }
        }

        public static IEnumerable<BookSet> ByOne(this BookSet self)
        {
            foreach(BookSet v in (BookSet[])(Enum.GetValues(typeof(BookSet))))
            {
                if((self & v) == v)
                    yield return v;
            }
        }

        public static IEnumerable<BookSet> ByTwo(this BookSet self)
        {
            IEnumerable<BookSet> byOne = self.ByOne();
            return byOne.SelectMany((first, i) => byOne.Skip(i + 1).Select(second => first | second));
        }

        public static IEnumerable<BookSet> ByThree(this BookSet self)
        {
            IEnumerable<BookSet> byOne = self.ByOne();
            return byOne.SelectMany((first, i) =>
                      byOne.Skip(i + 1).SelectMany((second, j) =>
                      byOne.Skip(i + 1 + j + 1).Select(third => first | second | third)));

        }

        public static int Variety(this BookSet self)
        {
            return BitOperations.PopCount((nuint)self);
        }
    }


}
