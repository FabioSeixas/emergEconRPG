
namespace econrpg
{
    public class Inventory
    {
        private int agentId;
        private List<InventoryItem> inventoryItems = new List<InventoryItem>();
        private double money;

        public List<InventoryItem> InventoryItems
        {
            get { return this.inventoryItems; }
        }

        public double Money
        {
            get { return this.money; }
        }

        public Inventory(int AgentId, RoleProductionRules roleProductionRules)
        {
            this.money = Globals.inventoryStartMoney;
            this.agentId = AgentId;
            this.startInventory(roleProductionRules);
        }

        public void increaseMoney(double value)
        {
            this.money += value;
        }

        public void decreaseMoney(double value)
        {
            this.money -= value;
        }

        // public void printInventory(RoleCommodities roleCommodities)
        // {
        //     Console.WriteLine("This inventory have '" + this.money + "' of money");
        //     Console.WriteLine("These are the commodities in this inventory");
        //     Console.WriteLine("Name\tQntd\tId\tThreshold");
        //     foreach (InventoryItem item in this.inventoryItems)
        //     {
        //         RoleCommodity roleCommodity = roleCommodities.FindRoleCommodityById(item.getCommodityId());
        //         Console.WriteLine($"{item.getCommodityName()}\t{item.getInventoryLevel()}\t{item.getCommodityId()}\t{roleCommodity.getThreshold()}");
        //     }
        // }

        public int[] getItemPriceBelief(int itemId)
        {
            return this.findItemById(itemId)?.getPriceBeliefs();
        }

        private void startInventory(RoleProductionRules roleProductionRules)
        {
            inventoryItems.RemoveAll(item => true);
            List<Commodity> agentCommodities = new List<Commodity>();
            ProductionRule oneProductionRule = roleProductionRules.rulesByOutputAmount.First();
            agentCommodities.Add(Commodities.getOneById(oneProductionRule.OutputId));
            foreach (RecipeItem recipeItem in oneProductionRule.Resources)
            {
                agentCommodities.Add(Commodities.getOneById(recipeItem.CommodityId));
            }
            foreach (Commodity commodity in agentCommodities)
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
            if (foundItem is null)
            {
                Commodity foundCommodity = Commodities.getOneById(commodityId);
                foundItem = this.addCommodityToInventory(foundCommodity);
                return foundItem.increaseQuantity(increaseAmount);
            };
            return foundItem.increaseQuantity(increaseAmount);
        }
        public void adjustInventoryLevelsAccordingToProductionRule(ProductionRule rule)
        {
            foreach (RecipeItem recipeItem in rule.Resources)
            {
                decreaseInventoryItemLevel(recipeItem.CommodityId, recipeItem.Amount);
            }
            increaseInventoryItemLevel(rule.OutputId, rule.OutputAmount);
        }
        private int getAmountBeyondThreshold(int inventoryLevel, int threshold, bool isProduced)
        {
            int amount;
            if (isProduced)
            {
                amount = inventoryLevel - threshold;
            }
            else
            {
                amount = threshold - inventoryLevel;
            }
            return amount;
        }
        private Offer generateOneOffer(int commodityId, String offerType)
        {
            InventoryItem item = this.findItemById(commodityId);
            if (item is null) throw new Exception("Inventory item with id " + commodityId + " was not found.");
            if (!offerType.Equals("ask") && !offerType.Equals("bid")) throw new Exception("offerType must be bid or ask");
            return new Offer(
                offerType,
                commodityId,
                // TODO: Need to get the amount beyond threshold for type "bid"
                // See previous version of Inventory class in git history.
                item.getInventoryLevel(),
                item.getValueFromPriceBeliefs(),
                this.agentId
            );
        }
        public List<Offer> generateOffers(ProductionRule productionRule)
        {
            List<Offer> itemsToTrade = new List<Offer>();
            itemsToTrade.Add(generateOneOffer(productionRule.OutputId, "ask"));
            foreach (RecipeItem recipeItem in productionRule.Resources)
            {
                itemsToTrade.Add(generateOneOffer(recipeItem.CommodityId, "bid"));
            }
            return itemsToTrade;
        }
    }
}
