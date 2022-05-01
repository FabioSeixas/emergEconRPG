
namespace econrpg
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] commoditiesNameList = {"Food", "Ore", "Tools", "Wood"};
            Commodities.startCommodities(commoditiesNameList);

            for (int i = 0; i < Globals.numberOfAgents; i++)
            {
                Console.WriteLine("\nThis is the Agente number " + (i + 1));
                Agent myAgent = new Agent();
                String roleName = myAgent.getCurrentRoleName();
                Console.WriteLine("The role of my agent is " + roleName);
                myAgent.printInventory();
                Console.WriteLine(myAgent.performProduction());
                myAgent.printInventory();
                Console.WriteLine("Is some Item beyond the threshold? " + myAgent.isTradeNeeded());

            }
        }
    }
}








