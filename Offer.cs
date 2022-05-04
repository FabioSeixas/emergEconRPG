namespace econrpg
{
    public class Offer
    {
        String type;
        int commodityId;
        int amount;
        double price;

        public Offer(String type, int commodityId, int amount, double price)
        {
            this.type = type;
            this.commodityId = commodityId;
            this.amount = amount;
            this.price = price;
        }

    }
}