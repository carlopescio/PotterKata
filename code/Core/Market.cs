namespace Potter
{
    internal class Market
    {
        internal void AddSales(IEnumerable<OfferToSell> sales)
        {
            forSale.AddRange(sales);
        }

        internal void AddPurchases(IEnumerable<OfferToBuy> purchases)
        {
            forPurchase.AddRange(purchases);
        }

        internal bool Trade()
        {
            ArrangeSalesByWorth();

            List<Deal> confirmedDeals = [];
            List<Party> busyParties = [];

            while(forSale.Count > 0 && forPurchase.Count > 0)
            {
                OfferToSell bestSale = ExtractBestSale();
                Deal? d = Deal.AttemptBetween(bestSale, forPurchase);
                if(d != null)
                {
                    confirmedDeals.Add(d);
                    List<Party> newParties = d.Parties().ToList();
                    busyParties.AddRange(newParties);
                    forPurchase.RemoveAll(p => newParties.Contains(p.From));
                    forSale.RemoveAll(p => newParties.Contains(p.From));
                }
            }

            foreach(var d in confirmedDeals)
                d.Seal();

            return confirmedDeals.Count != 0;
        }

        private OfferToSell ExtractBestSale()
        {
            int bestSaleIndex = forSale.Count - 1;
            OfferToSell bestSale = forSale[bestSaleIndex];
            forSale.RemoveAt(bestSaleIndex);
            return bestSale;
        }

        private void ArrangeSalesByWorth()
        {
            forSale.Sort((a, b) => b.Delta - a.Delta);
        }


        private List<OfferToSell> forSale = [];
        private List<OfferToBuy> forPurchase = [];
    }
}
