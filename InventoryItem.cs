namespace econrpg
{
    public class InventoryItem
    {
        int quantity;
        int[] priceBelief;
        Commodity commodity;

        public InventoryItem(Commodity commodity)
        {
            this.commodity = commodity;
            this.quantity = 0;
            this.priceBelief = new int[2] {0, 10}; 
        }

        public int getInventoryLevel()
        {
            return this.quantity;
        }       

        public int[] getPriceBeliefs()
        {
            return this.priceBelief;
        }

        public int getCommodityId()
        {
            return this.commodity.getId();
        }
       
        public String getCommodityName()
        {
            return this.commodity.getName();
        }
    }
}