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
        public int getOffersTotalAmount()
        {
            return this.offers.Aggregate(0, (total, next) => total + next.amount);
        }
        public bool thereStillUnfilledOffers()
        {
            return this.offers.Exists(x => x.getUnfilledAmount() > 0);
        }
        public void addOffer(Offer offer)
        {
            offers.Add(offer);
            this.suffleBook();
        }
        public void cleanBook()
        {
            offers.Clear();
        }
        public void printOffers()
        {
            Console.WriteLine("These are the offers in this Book");
            Console.WriteLine("AgId\tCmmId\tType\tAmount\tPrice");
            foreach(Offer offer in this.offers)
            {
                Console.WriteLine($"{offer.agentId}\t{offer.commodityId}\t{offer.type}\t{offer.amount}\t{offer.price}");
            }
        }

    }
}