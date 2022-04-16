using System.Collections.Generic;

namespace econrpg
{
    public class Inventory
    {
        private List<InventoryItem> inventoryItems = new List<InventoryItem>();

        private double money;

        public Inventory()
        {
            this.money = 100;
        }

        public void increaseMoney(double value)
        {
            this.money += value;
        }

        public void decreaseMoney(double value)
        {
            this.money -= value;
        }

        public void printInventory()
        {   
            Console.WriteLine("These are the commodities in this inventory");
            Console.WriteLine("Name\tQntd\tId");
            foreach (InventoryItem item in this.inventoryItems)
            {
                Console.WriteLine($"{item.getCommodityName()}\t{item.getInventoryLevel()}\t{item.getCommodityId()}");
            }
        }

        public void startInventory(List<Commodity> commodities)
        {
            inventoryItems.RemoveAll(item => true);
            foreach (Commodity commodity in commodities)
            {   
                InventoryItem newItem = new InventoryItem(commodity);
                newItem.increaseQuantity(1);
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
    }
}