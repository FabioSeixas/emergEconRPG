namespace econrpg
{
    public class ClearingHouse
    {
        private List<Offer> offersList; 
        public ClearingHouse() {
            this.offersList = new List<Offer>();
        }

        public void receiveOffers(List<Offer> offers)
        {
            foreach(Offer offer in offers)
            {
                this.offersList.Add(offer);
            }
        }

        private IEnumerable<int> getCommoditiesIds()
        {
            IEnumerable<int> ids = offersList.Select(x => x.commodityId);
            return ids.Distinct();
        }

        public void printOffers()
        {
            Console.WriteLine("These are the offers in the Clearing House");
            Console.WriteLine("AgId\tCmmId\tType\tAmount\tPrice");
            foreach(Offer offer in offersList)
            {
                Console.WriteLine($"{offer.agentId}\t{offer.commodityId}\t{offer.type}\t{offer.amount}\t{offer.price}");
            }
        }

        public void printOffersSubset(List<Offer> offers)
        {
            Console.WriteLine("These are the offers in the Subset");
            Console.WriteLine("AgId\tCmmId\tType\tAmount\tPrice");
            foreach(Offer offer in offers)
            {
                Console.WriteLine($"{offer.agentId}\t{offer.commodityId}\t{offer.type}\t{offer.amount}\t{offer.price}");
            }
        }

        private List<Offer> filterOffersByCommodityId(int commodityId)
        {
            return offersList.FindAll(x => x.commodityId == commodityId);
        }

        private List<Offer> getOffersByType(String type, List<Offer> offers)
        {
            if (type != "bid" & type != "ask") throw new Exception("Wrong offer type");
            return offers.FindAll(x => x.type == type);
        }

        public void resolveOffers()
        {
            this.printOffers();
            IEnumerable<int> commoditiesIds = this.getCommoditiesIds();
            foreach (int commodityId in commoditiesIds)
            {
                Console.WriteLine("\nResult of " + Commodities.getOneById(commodityId).getName() + " commodity");
                List<Offer> commodityOffers = this.filterOffersByCommodityId(commodityId);
                List<Offer> bidOffers = this.getOffersByType("bid", commodityOffers);
                List<Offer> askOffers = this.getOffersByType("ask", commodityOffers);
                Console.WriteLine("BID OFFERS: ");
                this.printOffersSubset(bidOffers);
                Console.WriteLine("ASK OFFERS: ");
                this.printOffersSubset(askOffers);
            }
        }




    }
}