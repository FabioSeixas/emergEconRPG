namespace econrpg
{
    public class Book
    {
        public String type;
        public int commodityId;
        List<Offer> offers;

        public Book(String type, int commodityId)
        {
            this.type = type;
            this.commodityId = commodityId;
            this.offers = new List<Offer>();
        }

        public void addOffer(Offer offer)
        {
            offers.Add(offer);
        }
        public void cleanBook()
        {
            offers.Clear();
        }
        public void printOffers()
        {
            Console.WriteLine("These are the offers in the Clearing House");
            Console.WriteLine("AgId\tCmmId\tType\tAmount\tPrice");
            foreach(Offer offer in this.offers)
            {
                Console.WriteLine($"{offer.agentId}\t{offer.commodityId}\t{offer.type}\t{offer.amount}\t{offer.price}");
            }
        }

    }
}