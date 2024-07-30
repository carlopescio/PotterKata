namespace Potter
{
    internal class Pricing
    {
        internal static int DiscountedPrice(int bookVariety)
        {
            return discountedPrice[bookVariety + ofs];
        }

        internal static decimal ToNominalPrice(int percentPrice)
        {
            return (nominalPrice * percentPrice) / 100;
        }

        internal static readonly int minDelta;

        private static readonly int ofs = 3;
        private static readonly int[] discountedPrice = [-3, -2, -1, 0, 100, 2 * 95, 3 * 90, 4 * 80, 5 * 75, 0];

        private static readonly Decimal nominalPrice = 8;


        static Pricing()
        {
            minDelta = int.MaxValue;
            for(int i = ofs + 1; i < discountedPrice.Length - 1; ++i)
            {
                int d = discountedPrice[i] - discountedPrice[i - 1];
                if(d < minDelta)
                    minDelta = d;
            }
        }
    }
}
