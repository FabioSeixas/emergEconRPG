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
            Console.WriteLine("Name\tQntd");
            foreach (InventoryItem item in this.inventoryItems)
            {
                Console.WriteLine($"{item.getCommodityName()}\t{item.getInventoryLevel()}");
            }
        }

        public void startInventory(List<Commodity> commodities)
        {
            foreach (Commodity commodity in commodities)
            {   
                InventoryItem newItem = new InventoryItem(commodity);
                this.addInventoryItem(newItem);
            }
        }
        public void addInventoryItem(InventoryItem newItem)
        {
            this.inventoryItems.Add(newItem);
        }

        private InventoryItem? findItemById(int inventoryItemId)
        {
            InventoryItem? foundItem = inventoryItems.Find(item => item.getCommodityId() == inventoryItemId);
            if (foundItem is null) return null; 
            return foundItem;
        }

        public int getInventoryItemLevel(int inventoryItemId)
        {
            InventoryItem? foundItem = this.findItemById(inventoryItemId);
            if (foundItem is null) return 0;
            return foundItem.getInventoryLevel();
        } 
    }
}