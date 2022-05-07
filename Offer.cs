namespace econrpg
{
    public class Offer: IComparable<Offer>
    {
        public String type;
        public int commodityId;
        public int amount;
        public int filledAmount;
        public int agentId;
        public double price;

        public Offer(String type, int commodityId, int amount, double price, int agentId)
        {
            this.agentId = agentId;
            this.type = type;
            this.commodityId = commodityId;
            this.amount = amount;
            this.price = price;
            this.filledAmount = 0;
        }
        public int CompareTo(Offer otherOffer)
        {
            if (otherOffer == null) return 1;
            return this.price.CompareTo(otherOffer.price);
        }
        public int getUnfilledAmount()
        {
            return this.amount - this.filledAmount;
        }

    }
}