namespace econrpg
{
    public class Role
    {
        static Random random = new Random();
        static String[] options = { "Miner", "Woodcutter", "Farmer", "Blacksmith"};
        static public Role GetRandomRole()
        {   
            switch (options[random.Next(options.Length)])
            {
                case "Miner":
                    return new Miner();
                case "Farmer":
                    return new Farmer();
                case "Woodcutter":
                    return new Woodcutter();
                case "Blacksmith":
                    return new Blacksmith();
                default:
                    return new Role();
            }
        }

        internal String name = "no role";
        internal List<Commodity> commodities = new List<Commodity>();

        internal void startRoleCommodities(String[] commoditiesName)
        {
            foreach (String name in commoditiesName)
            {   
                Commodity? foundCommodity = Commodities.getOneByName(name);
                if (foundCommodity is null) continue;
                this.commodities.Add(foundCommodity);
            }
        }

        public virtual String production(Inventory inventory)
        {
            return "[Info]: No production method yet";
        }

        public String GetName()
        {
            return this.name;
        }

        public List<Commodity> GetCommodities()
        {
            return this.commodities;
        }

    }

    class Miner : Role
    {
        public Miner()
        {
            this.name = "Miner";
            this.startRoleCommodities(new String[] {"Food", "Tools", "Ore"});
        }

        public override String production(Inventory inventory)
        {
            Commodity foodCommodity = this.commodities[0];
            Commodity toolsCommodity = this.commodities[1];
            Commodity oreCommodity = this.commodities[2];

            int foodLevel = inventory.getInventoryItemLevel(foodCommodity.getId());
            int toolsLevel = inventory.getInventoryItemLevel(toolsCommodity.getId());

            if (foodLevel > 1 && toolsLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(foodCommodity.getId(), 2);
                inventory.decreaseInventoryItemLevel(toolsCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(oreCommodity.getId(), 2);
                return "[Success]: 2 units of Food and 1 unit of Tools consumed \n 2 units of Ore produced";
            } else if (foodLevel > 0 && toolsLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(foodCommodity.getId(), 1);
                inventory.decreaseInventoryItemLevel(toolsCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(oreCommodity.getId(), 1);
                return "[Success]: 1 unit of Food and 1 unit of Tools consumed / 1 unit of Ore produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }

    class Farmer : Role
    {
        public Farmer()
        {
            this.name = "Farmer";
            this.startRoleCommodities(new String[] {"Wood", "Tools", "Food"});
        }

         public override String production(Inventory inventory)
        {
            Commodity woodCommodity = this.commodities[0];
            Commodity toolsCommodity = this.commodities[1];
            Commodity foodCommodity = this.commodities[2];

            int woodLevel = inventory.getInventoryItemLevel(woodCommodity.getId());
            int toolsLevel = inventory.getInventoryItemLevel(toolsCommodity.getId());

            if (woodLevel > 1 && toolsLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(woodCommodity.getId(), 2);
                inventory.decreaseInventoryItemLevel(toolsCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(foodCommodity.getId(), 2);
                return "[Success]: 2 units of Wood and 1 unit of Tools consumed \n 2 units of Food produced";
            } else if (woodLevel > 0 && toolsLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(woodCommodity.getId(), 1);
                inventory.decreaseInventoryItemLevel(toolsCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(foodCommodity.getId(), 1);
                return "[Success]: 1 unit of Wood and 1 unit of Tools consumed / 1 unit of Food produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }

    class Woodcutter : Role
    {
        public Woodcutter()
        {
            this.name = "Woodcutter";
            this.startRoleCommodities(new String[] {"Food", "Tools", "Wood"});
        }
    }
  
    class Blacksmith : Role
    {
        public Blacksmith()
        {
            this.name = "Blacksmith";
            this.startRoleCommodities(new String[] {"Food", "Ore", "Tools", "Wood"});
        }
    }
}
