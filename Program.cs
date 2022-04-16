
namespace econrpg
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] commoditiesNameList = {"Food", "Ore", "Tools", "Wood"};
            Commodities.startCommodities(commoditiesNameList);

            Agent myAgent = new Agent();
            String roleName = myAgent.getCurrentRoleName();
            Console.WriteLine("\nThe role of my agent is " + roleName);
            myAgent.printInventory();
            Console.WriteLine(myAgent.performProduction());
            myAgent.printInventory();
            
            Agent myAgent2 = new Agent();
            String roleName2 = myAgent2.getCurrentRoleName();
            Console.WriteLine("\nThe role of my SECOND agent is " + roleName2);
            myAgent2.printInventory();
            Console.WriteLine(myAgent2.performProduction());
            myAgent2.printInventory();

            
            // Commodity myCommodity = new Commodity("wood");
            // Console.WriteLine($"The id of my new commodity is: {myCommodity.getId()}");
            // Commodity myCommodity2 = new Commodity("food");
            // Console.WriteLine($"The id of my new commodity is: {myCommodity2.getId()}");
            // Commodity myCommodity3 = new Commodity("rice");
            // Console.WriteLine($"The id of my new commodity is: {myCommodity3.getId()}");


        }
    }
}








