using System.Collections.Generic;

namespace econrpg
{
    public class Inventory
    {
        private int agentId;
        private List<InventoryItem> inventoryItems = new List<InventoryItem>();

        private double money;

        public Inventory(int AgentId, List<Commodity> commodities)
        {
            this.money = 100;
            this.agentId = AgentId;
            this.startInventory(commodities);
        }

        public void increaseMoney(double value)
        {
            this.money += value;
        }

        public void decreaseMoney(double value)
        {
            this.money -= value;
        }

        public void printInventory(RoleCommodities roleCommodities)
        {   
            Console.WriteLine("These are the commodities in this inventory");
            Console.WriteLine("Name\tQntd\tId\tThreshold");
            foreach (InventoryItem item in this.inventoryItems)
            {
                RoleCommodity roleCommodity = roleCommodities.FindRoleCommodityById(item.getCommodityId());
                Console.WriteLine($"{item.getCommodityName()}\t{item.getInventoryLevel()}\t{item.getCommodityId()}\t{roleCommodity.getThreshold()}");
            }
        }

        public int[] getItemPriceBelief(int itemId)
        {   
            return this.findItemById(itemId)?.getPriceBeliefs(); 
        }

        private void startInventory(List<Commodity> commodities)
        {
            inventoryItems.RemoveAll(item => true);
            foreach (Commodity commodity in commodities)
            {   
                InventoryItem newItem = new InventoryItem(commodity);
                newItem.increaseQuantity(Globals.inventoryItemStartAmount);
                this.addInventoryItem(newItem);
            }
        }
        private void addInventoryItem(InventoryItem newItem)
        {
            this.inventoryItems.Add(newItem);
        }

        public InventoryItem addCommodityToInventory(Commodity commodity)
        {
            InventoryItem newItem = new InventoryItem(commodity);
            this.addInventoryItem(newItem);
            return newItem;
        }

        public InventoryItem? findItemById(int inventoryItemId)
        {
            InventoryItem? foundItem = inventoryItems.Find(item => item.getCommodityId() == inventoryItemId);
            if (foundItem is null) return null; 
            return foundItem;
        }
        
        public InventoryItem? findItemByName(String inventoryItemName)
        {
            InventoryItem? foundItem = inventoryItems.Find(item => item.getCommodityName() == inventoryItemName);
            if (foundItem is null) return null; 
            return foundItem;
        }

        public int getInventoryItemLevel(int inventoryItemId)
        {
            InventoryItem? foundItem = this.findItemById(inventoryItemId);
            if (foundItem is null) return 0;
            return foundItem.getInventoryLevel();
        }

        public int decreaseInventoryItemLevel(int commodityId, int decreaseAmount)
        {
            InventoryItem? foundItem = this.findItemById(commodityId);
            if (foundItem is null) return 0;
            return foundItem.decreaseQuantity(decreaseAmount);
        }
        public int increaseInventoryItemLevel(int commodityId, int increaseAmount)
        {
            InventoryItem? foundItem = this.findItemById(commodityId);
            if (foundItem is null) {
                Commodity foundCommodity = Commodities.getOneById(commodityId);
                foundItem = this.addCommodityToInventory(foundCommodity);
                return foundItem.increaseQuantity(increaseAmount);
            };
            return foundItem.increaseQuantity(increaseAmount);
        }
        private int getAmountBeyondThreshold(int inventoryLevel, int threshold, bool isProduced)
        {
            int amount;
            if (isProduced)
            {
                amount = inventoryLevel - threshold;
            } else {
                amount = threshold - inventoryLevel;
            }
            return amount;
        }
        public List<Offer> generateOffers(RoleCommodities roleCommodities)
        {   
            List<Offer> itemsToTrade = new List<Offer>();
            foreach (RoleCommodity roleCommodity in roleCommodities.GetRoleCommodities())
            {
                int fullThreshold = roleCommodity.getThreshold();
                InventoryItem item = this.findItemById(roleCommodity.getCommodityId());
                int inventoryLevel = item.getInventoryLevel();
                bool isProduced = roleCommodity.isProduced();
                int amountBeyondThreshold = this.getAmountBeyondThreshold(inventoryLevel, fullThreshold, isProduced);
                if (amountBeyondThreshold > 0)
                {
                    Offer newOffer = new Offer(
                        isProduced ? "ask" : "bid",
                        roleCommodity.getCommodityId(),
                        amountBeyondThreshold,
                        item.getValueFromPriceBeliefs(),
                        this.agentId
                    );
                    itemsToTrade.Add(newOffer);
                }
            }
            return itemsToTrade;
        }
    }
}
