namespace econrpg
{
    public class Book
    {
        private static Random random = new Random();
        public String type;
        public int commodityId;
        List<Offer> offers;

        public Book(String type, int commodityId)
        {
            this.type = type;
            this.commodityId = commodityId;
            this.offers = new List<Offer>();
        }
        private void suffleBook()
        {
            int n = this.offers.Count;  
            while (n > 1) {  
                n--;  
                int k = random.Next(n + 1);  
                Offer value = this.offers[k];  
                this.offers[k] = this.offers[n];  
                this.offers[n] = value;  
            }  
        }
        public void sortOffers()
        {
            this.offers.Sort();
            if (this.type == "bid") this.offers.Reverse();
        }

        public void finishOffers()
        {
            foreach (Offer offer in this.offers)
            {
                Agent agent = Agent.getAgentById(offer.agentId);
                agent.receiveOfferResult(offer);
            }
            this.cleanBook();
        }
        public int getOffersTotalAmount()
        {
            return this.offers.Aggregate(0, (total, next) => total + next.amount);
        }
        public bool stillOpenOffers()
        {
            return this.offers.Exists(x => x.open);
        }
        public Offer getOpenOfferOnTop()
        {
            foreach (Offer offer in this.offers)
            {
                if (offer.open)
                {
                    return offer;
                }
            }
            // it should not get here
            return this.offers[0];
        }
        public void addOffer(Offer offer)
        {
            this.offers.Add(offer);
            this.suffleBook();
        }
        private void cleanBook()
        {
            this.offers.Clear();
        }
        public void printOffers()
        {
            Console.WriteLine("\nThese are the offers in this Book");
            Console.WriteLine("Commodity: " + Commodities.getOneById(this.commodityId).getName());
            Console.WriteLine("AgId\tCmmId\tType\tAmount\tPrice\tOpen");
            foreach(Offer offer in this.offers)
            {
                Console.WriteLine($"{offer.agentId}\t{offer.commodityId}\t{offer.type}\t{offer.amount}\t{offer.price}\t{offer.open}");
            }
        }

    }
}