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
        internal String[] commodities = {"No commodity at all"};

        public String GetName()
        {
            return this.name;
        }

        public List<Commodity> getRoleCommodities()
        {
            List<Commodity> commoditiesList = new List<Commodity>();
            foreach (String commodity in this.commodities)
            {
                commoditiesList.Add(new Commodity(commodity));
            }
            return commoditiesList;
        }

    }

    class Miner : Role
    {
        public Miner()
        {
            this.name = "Miner";
            this.commodities = new String[] {"Food", "Tools", "Ore"};
        }
    }

    class Farmer : Role
    {
        public Farmer()
        {
            this.name = "Farmer";
            this.commodities = new String[] {"Food", "Tools"};
        }
    }

    class Woodcutter : Role
    {
        public Woodcutter()
        {
            this.name = "Woodcutter";
            this.commodities = new String[] {"Food", "Tools", "Wood"};
        }
    }
  
    class Blacksmith : Role
    {
        public Blacksmith()
        {
            this.name = "Blacksmith";
            this.commodities = new String[] {"Food", "Ore", "Wood", "Tools"};
        }
    }
}
