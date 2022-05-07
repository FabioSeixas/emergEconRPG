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
        public double wallet;
        public bool open;

        public Offer(String type, int commodityId, int amount, double price, int agentId)
        {
            this.agentId = agentId;
            this.type = type;
            this.commodityId = commodityId;
            this.amount = amount;
            this.price = price;
            this.filledAmount = 0;
            this.wallet = 0;
            this.open = true;
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
        private void closeOffer()
        {
            this.open = false;
        }
        public void trade(int amountTraded, double price)
        {
            if (amountTraded > this.amount) throw new Exception(
                "\n Amount traded '" + amountTraded + "' higher than offer amount '" + this.amount + "'"
                );
            this.filledAmount += amountTraded;
            this.wallet = price * amountTraded;
            if (this.getUnfilledAmount() < 1) this.closeOffer();
        }
    }
}