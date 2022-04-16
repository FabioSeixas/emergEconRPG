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
        internal List<RoleCommodity> roleCommodities = new List<RoleCommodity>();

        internal void startRoleCommodities(
            String[] commoditiesName, 
            int[] commoditiesThreshold, 
            bool[] commoditiesProduced
            )
        {
            for (int index = 0; index < commoditiesName.Length; index++)
            {   
                Commodity? foundCommodity = Commodities.getOneByName(commoditiesName[index]);
                this.roleCommodities.Add(
                    new RoleCommodity(
                        foundCommodity, 
                        commoditiesThreshold[index], 
                        commoditiesProduced[index]
                        )
                    );
            }
        }

        public virtual String production(Inventory inventory)
        {
            return "[Info]: No production method for the parent Role class";
        }

        public String GetName()
        {
            return this.name;
        }

        public List<RoleCommodity> GetRoleCommodities()
        {
            return this.roleCommodities;
        }
        public List<Commodity> GetCommodities()
        {   
            List<Commodity> commodities = new List<Commodity>();
            foreach (RoleCommodity roleCommodity in this.roleCommodities)
            {
                commodities.Add(roleCommodity.GetCommodity());
            } 
            return commodities;
        }

        public RoleCommodity FindRoleCommodityById(int id)
        {
            try
            {
                RoleCommodity foundRoleCommodity = this.roleCommodities.Find(item => item.getCommodityId() == id);
                foundRoleCommodity.getCommodityId();
                return foundRoleCommodity;
            }
            catch (NullReferenceException e)
            {   
                Console.WriteLine($"[Error]: Commodity with id {id} was not found");
                throw e;
            }
        }

    }

    class Miner : Role
    {
        public Miner()
        {
            this.name = "Miner";
            String[] commoditiesNames = new String[] {"Food", "Tools", "Ore"};
            int[] commoditiesThresholds = new int[] {2, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true};
            this.startRoleCommodities(commoditiesNames, commoditiesThresholds, commoditiesProduced);
        }

        public override String production(Inventory inventory)
        {
            Commodity foodCommodity = this.roleCommodities[0].GetCommodity();
            Commodity toolsCommodity = this.roleCommodities[1].GetCommodity();
            Commodity oreCommodity = this.roleCommodities[2].GetCommodity();

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

            String[] commoditiesNames = new String[] {"Wood", "Tools", "Food"};
            int[] commoditiesThresholds = new int[] {2, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true};
            this.startRoleCommodities(commoditiesNames, commoditiesThresholds, commoditiesProduced);
        }

        public override String production(Inventory inventory)
        {
            Commodity woodCommodity = this.roleCommodities[0].GetCommodity();
            Commodity toolsCommodity = this.roleCommodities[1].GetCommodity();
            Commodity foodCommodity = this.roleCommodities[2].GetCommodity();

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
            String[] commoditiesNames = new String[] {"Food", "Tools", "Wood"};
            int[] commoditiesThresholds = new int[] {2, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true};
            this.startRoleCommodities(commoditiesNames, commoditiesThresholds, commoditiesProduced);
        }

        public override String production(Inventory inventory)
        {
            Commodity foodCommodity = this.roleCommodities[0].GetCommodity();
            Commodity toolsCommodity = this.roleCommodities[1].GetCommodity();
            Commodity woodCommodity = this.roleCommodities[2].GetCommodity();

            int foodLevel = inventory.getInventoryItemLevel(foodCommodity.getId());
            int toolsLevel = inventory.getInventoryItemLevel(toolsCommodity.getId());

            if (foodLevel > 1 && toolsLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(foodCommodity.getId(), 2);
                inventory.decreaseInventoryItemLevel(toolsCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(woodCommodity.getId(), 2);
                return "[Success]: 2 units of Food and 1 unit of Tools consumed \n 2 units of Wood produced";
            } else if (foodLevel > 0 && toolsLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(foodCommodity.getId(), 1);
                inventory.decreaseInventoryItemLevel(toolsCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(woodCommodity.getId(), 1);
                return "[Success]: 1 unit of Food and 1 unit of Tools consumed / 1 unit of Wood produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }
  
    class Blacksmith : Role
    {
        public Blacksmith()
        {
            this.name = "Blacksmith";
            String[] commoditiesNames = new String[] {"Food", "Ore", "Tools", "Wood"};
            int[] commoditiesThresholds = new int[] {2, 1, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true, false};
            this.startRoleCommodities(commoditiesNames, commoditiesThresholds, commoditiesProduced);
        }

        public override String production(Inventory inventory)
        {
            Commodity foodCommodity = this.roleCommodities[0].GetCommodity();
            Commodity oreCommodity = this.roleCommodities[1].GetCommodity();
            Commodity toolsCommodity = this.roleCommodities[2].GetCommodity();
            Commodity woodCommodity = this.roleCommodities[3].GetCommodity();

            int foodLevel = inventory.getInventoryItemLevel(foodCommodity.getId());
            int oreLevel = inventory.getInventoryItemLevel(oreCommodity.getId());
            int woodLevel = inventory.getInventoryItemLevel(woodCommodity.getId());

            if (foodLevel > 1 && oreLevel > 0 && woodLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(foodCommodity.getId(), 2);
                inventory.decreaseInventoryItemLevel(oreCommodity.getId(), 1);
                inventory.decreaseInventoryItemLevel(woodCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(toolsCommodity.getId(), 2);
                return "[Success]: 2 units of Food, 1 unit of Wood and 1 unit of Ore consumed \n 2 units of Tools produced";
            } else if (foodLevel > 0 && oreLevel > 0 && woodLevel > 0)
            {
                inventory.decreaseInventoryItemLevel(foodCommodity.getId(), 1);
                inventory.decreaseInventoryItemLevel(oreCommodity.getId(), 1);
                inventory.decreaseInventoryItemLevel(woodCommodity.getId(), 1);
                inventory.increaseInventoryItemLevel(toolsCommodity.getId(), 1);
                return "[Success]: 1 unit of Food, 1 unit of Wood and 1 unit of Ore consumed \n 1 unit of Tools produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }
}
