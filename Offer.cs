namespace econrpg
{
    public class Offer
    {
        public String type;
        public int commodityId;
        public int amount;
        public int agentId;
        public double price;

        public Offer(String type, int commodityId, int amount, double price, int agentId)
        {
            this.agentId = agentId;
            this.type = type;
            this.commodityId = commodityId;
            this.amount = amount;
            this.price = price;
        }

    }
}