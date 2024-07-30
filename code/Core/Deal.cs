using System.Text;

namespace Potter
{
    internal class Deal(OfferToSell Sale, List<OfferToBuy> Purchases)
    {
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("Sale: ");
            sb.Append(Sale.ToString());
            sb.Append("; Purchases: [ ");
            foreach(var p in Purchases)
            {
                sb.Append(p.ToString());
                sb.Append("; ");
            }
            sb.Append(']');
            return sb.ToString();
        }

        internal void Seal()
        {
            Sale.Accepted();
            foreach(var p in Purchases)
                p.Accepted();
        }

        internal IEnumerable<Party> Parties()
        {
            yield return Sale.From;
            foreach(var p in Purchases)
                yield return p.From;
        }

        internal static Deal? AttemptBetween(OfferToSell bestSale, List<OfferToBuy> forPurchase)
        {
            List<OfferToBuy> candidatePurchases = bestSale.MatchingOffersByWorth(forPurchase);
            foreach(OfferToBuy bestBuy in candidatePurchases)
            {
                if(bestSale.Goods == bestBuy.Goods)
                    return new Deal(bestSale, [bestBuy]);
                else
                {
                    int newBalance = bestSale.Delta + bestBuy.Delta;
                    // this makes a huge performance difference
                    if(newBalance > -Pricing.minDelta)
                        return null;

                    OfferToSell adjustedSale = new(bestSale.From, bestSale.Goods ^ bestBuy.Goods, newBalance);
                    Deal? subDeal = AttemptBetween(adjustedSale, [.. forPurchase.Where(o => o.From != bestBuy.From)]);
                    if(subDeal != null)
                    {
                        subDeal.Purchases.Add(bestBuy);
                        return new Deal(bestSale, subDeal.Purchases);
                    }
                }
            }

            return null;
        }

        private List<OfferToBuy> Purchases { get; } = Purchases;
    }
}
