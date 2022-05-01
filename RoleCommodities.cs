namespace econrpg
{
    public class RoleCommodities
    {
        private List<RoleCommodity> itemsList = new List<RoleCommodity>();

        public RoleCommodities(
            String[] commoditiesName, 
            int[] commoditiesFullThreshold,
            int[] commoditiesNotFullThreshold, 
            bool[] commoditiesProduced
            )
        {
             for (int index = 0; index < commoditiesName.Length; index++)
            {   
                Commodity? foundCommodity = Commodities.getOneByName(commoditiesName[index]);
                this.itemsList.Add(
                    new RoleCommodity(
                        foundCommodity, 
                        commoditiesFullThreshold[index], 
                        commoditiesNotFullThreshold[index], 
                        commoditiesProduced[index]
                        )
                    );
            }
        }

        public bool ProductionReady(Inventory inventory, bool fullProduction = true)
        {   
            bool ready = true;
            List<RoleCommodity> rawMaterials = FindRoleCommoditiesByPurpose("consume");
            foreach (RoleCommodity item in rawMaterials)
            {
                int threshold = item.getThreshold(fullProduction);
                int inventoryLevel = inventory.getInventoryItemLevel(item.getCommodityId());
                ready = inventoryLevel >= threshold;
                if (!ready) break;
            }
            return ready;
        }

        public void AdjustInventoryLevels(Inventory inventory, bool fullProduction = true)
        {
            foreach (RoleCommodity item in this.itemsList)
            {
                int updateAmount = item.getThreshold(fullProduction);
                if (!item.isProduced())
                {
                    inventory.decreaseInventoryItemLevel(item.getCommodityId(), updateAmount);
                    continue;
                }
                if (item.isProduced())
                {
                    inventory.increaseInventoryItemLevel(item.getCommodityId(), updateAmount);
                    continue;
                }
            }
        }

        public List<RoleCommodity> GetRoleCommodities()
        {
            return this.itemsList;
        }
        public List<Commodity> GetCommodities()
        {   
            List<Commodity> commodities = new List<Commodity>();
            foreach (RoleCommodity roleCommodity in this.itemsList)
            {
                commodities.Add(roleCommodity.GetCommodity());
            } 
            return commodities;
        }
        public RoleCommodity FindRoleCommodityById(int id)
        {
            try
            {
                RoleCommodity? foundRoleCommodity = this.itemsList.Find(item => item.getCommodityId() == id);
                foundRoleCommodity?.getCommodityId();
                return foundRoleCommodity;
            }
            catch (NullReferenceException e)
            {   
                Console.WriteLine($"[Error]: Commodity with id {id} was not found");
                throw e;
            }
        }

        public List<RoleCommodity> FindRoleCommoditiesByPurpose(String purpose)
        {
            List<RoleCommodity> list = new List<RoleCommodity>();
            if (purpose != "consume" & purpose != "produce") { return list; }
            foreach (RoleCommodity item in this.itemsList)
            {
                if (purpose == "consume" & !item.isProduced())
                {
                    list.Add(item);
                    continue;
                } 
                if (purpose == "produce" & item.isProduced())
                {
                    list.Add(item);
                    continue;
                }
            }
            return list;
        }
        
    }
}