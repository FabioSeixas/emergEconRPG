namespace econrpg
{
    public abstract class Role
    {
        static Random random = new Random();
        private static String[] options = { "Miner", "Woodcutter", "Farmer", "Blacksmith"};
        public static Role GetRandomRole()
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
                    return new Miner();
            }
        }
        public abstract String Name { get; }
        protected String name;
        protected RoleCommodities roleCommodities;
        public List<Commodity> GetCommodities()
        {
            return this.roleCommodities.GetCommodities();
        }
        public RoleCommodities GetRoleCommodities()
        {
            return this.roleCommodities;
        }
        public abstract String production(Inventory inventory);

    }

    public class Miner : Role
    {   
        public Miner()
        {   
            this.name = "Miner";
            String[] commoditiesNames = new String[] {"Food", "Tools", "Ore"};
            int[] commoditiesFullThresholds = new int[] {2, 1, 2};
            int[] commoditiesNotFullThresholds = new int[] {1, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true};
            this.roleCommodities = new RoleCommodities(
                commoditiesNames, 
                commoditiesFullThresholds, 
                commoditiesNotFullThresholds, 
                commoditiesProduced
            );
        }

        public override String Name { get { return this.name; }}

        public override String production(Inventory inventory)
        {
            if (this.roleCommodities.ProductionReady(inventory))
            {
                this.roleCommodities.AdjustInventoryLevels(inventory);
                return "[Success]: 2 units of Food and 1 unit of Tools consumed \n 2 units of Ore produced";
            } else if (this.roleCommodities.ProductionReady(inventory, false))
            {
                this.roleCommodities.AdjustInventoryLevels(inventory, false);
                return "[Success]: 1 unit of Food and 1 unit of Tools consumed / 1 unit of Ore produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }

    sealed class Farmer : Role
    {
        public Farmer()
        {
            this.name = "Farmer";
            String[] commoditiesNames = new String[] {"Wood", "Tools", "Food"};
            int[] commoditiesFullThresholds = new int[] {2, 1, 2};
            int[] commoditiesNotFullThresholds = new int[] {1, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true};
            this.roleCommodities = new RoleCommodities(
                commoditiesNames, 
                commoditiesFullThresholds, 
                commoditiesNotFullThresholds, 
                commoditiesProduced
            );
        }
        public override String Name { get { return this.name; }}

        public override String production(Inventory inventory)
        {
            if (this.roleCommodities.ProductionReady(inventory))
            {
                Console.WriteLine("Full Production Started");
                this.roleCommodities.AdjustInventoryLevels(inventory);
                return "[Success]: 2 units of Wood and 1 unit of Tools consumed \n 2 units of Food produced";
            } else if (this.roleCommodities.ProductionReady(inventory, false))
            {
                this.roleCommodities.AdjustInventoryLevels(inventory, false);
                return "[Success]: 1 unit of Wood and 1 unit of Tools consumed / 1 unit of Food produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }

    sealed class Woodcutter : Role
    {
        public Woodcutter()
        {
            this.name = "Woodcutter";
            String[] commoditiesNames = new String[] {"Food", "Tools", "Wood"};
            int[] commoditiesFullThresholds = new int[] {2, 1, 2};
            int[] commoditiesNotFullThresholds = new int[] {1, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true};
            this.roleCommodities = new RoleCommodities(
                commoditiesNames, 
                commoditiesFullThresholds, 
                commoditiesNotFullThresholds, 
                commoditiesProduced
            );
        }
        public override String Name { get { return this.name; }}

        public override String production(Inventory inventory)
        {
            if (this.roleCommodities.ProductionReady(inventory))
            {
                Console.WriteLine("Full Production Started");
                this.roleCommodities.AdjustInventoryLevels(inventory);
                return "[Success]: 2 units of Food and 1 unit of Tools consumed \n 2 units of Wood produced";
            } else if (this.roleCommodities.ProductionReady(inventory, false))
            {
                this.roleCommodities.AdjustInventoryLevels(inventory, false);
                return "[Success]: 1 unit of Food and 1 unit of Tools consumed / 1 unit of Wood produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }
  
    sealed class Blacksmith : Role
    {
        public Blacksmith()
        {
            this.name = "Blacksmith";
            String[] commoditiesNames = new String[] {"Food", "Ore", "Tools", "Wood"};
            int[] commoditiesFullThresholds = new int[] {2, 1, 2, 1};
            int[] commoditiesNotFullThresholds = new int[] {1, 1, 1, 1};
            bool[] commoditiesProduced = new bool[] {false, false, true, false};
            this.roleCommodities = new RoleCommodities(
                commoditiesNames, 
                commoditiesFullThresholds, 
                commoditiesNotFullThresholds, 
                commoditiesProduced
            );
        }
        public override String Name { get { return this.name; }}

         public override String production(Inventory inventory)
        {
            if (this.roleCommodities.ProductionReady(inventory))
            {
                Console.WriteLine("Full Production Started");
                this.roleCommodities.AdjustInventoryLevels(inventory);
                return "[Success]: 2 units of Food, 1 unit of Wood and 1 unit of Ore consumed \n 2 units of Tools produced";
            } else if (this.roleCommodities.ProductionReady(inventory, false))
            {
                this.roleCommodities.AdjustInventoryLevels(inventory, false);
                return "[Success]: 1 unit of Food, 1 unit of Wood and 1 unit of Ore consumed \n 1 unit of Tools produced";
            }
            return "[Failure]: No enough resources in the inventory";
        }
    }
}
