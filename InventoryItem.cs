namespace econrpg
{
    public class InventoryItem
    {
        static Random random = new Random();
        int quantity;
        int[] priceBelief;
        Commodity commodity;

        public InventoryItem(Commodity commodity)
        {
            this.commodity = commodity;
            this.quantity = 0;
            this.priceBelief = new int[2] {Globals.defaultPriceBeliefLower, Globals.defaultPriceBeliefHigher};
        }

        public int getInventoryLevel()
        {
            return this.quantity;
        }       
        public int[] getPriceBeliefs()
        {
            return this.priceBelief;
        }       
        public double getValueFromPriceBeliefs()
        {
            int value = random.Next(this.priceBelief[0], this.priceBelief[1]);
            return value;
        }       
        public int getCommodityId()
        {
            return this.commodity.getId();
        }
       
        public String getCommodityName()
        {
            return this.commodity.getName();
        }

        public int decreaseQuantity(int amount)
        {   
            this.quantity -= amount;
            return this.quantity;
        }
       
        public int increaseQuantity(int amount)
        {   
            this.quantity += amount;
            return this.quantity;
        }
    }
}