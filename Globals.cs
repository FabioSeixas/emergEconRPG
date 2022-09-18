namespace econrpg
{
    static public class Globals
    {
        static public String storageDir = "storage";
        static public String rulesDir = "rules";
        static public int inventoryItemStartAmount = 2;
        static public int inventoryStartMoney = 20;
        static public int defaultPriceBeliefLower = 1;
        static public int defaultPriceBeliefHigher = 10;
        static public int numberOfAgents = 100;
        static public int numberOfRounds = 10;

        static Random random = new Random();

        public enum Roles
        {
            Miner,
            Farmer,
            Woodcutter,
            Blacksmith
        }
        static private int totalMembersInEnum = Enum.GetNames(typeof(Roles)).Length;
        static public Roles getRoleByName(String name)
        {
            List<String> names = Enum.GetNames(typeof(Roles)).ToList();
            int index = names.IndexOf(name);
            if (index < 0) throw new Exception("Role with name " + name + " was not found.");
            return (Roles)index;
        }
        static public Roles GetRandomRole() 
        {
            int randomValue = random.Next(totalMembersInEnum);
            Roles randomRole = (Roles)randomValue;
            return randomRole;
        }

    }
}
