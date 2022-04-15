
namespace econrpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Agent myAgent = new Agent();
            String roleName = myAgent.getCurrentRoleName();
            Console.WriteLine("The role of my agent is " + roleName);
            myAgent.printInventory();
            // Commodity myCommodity = new Commodity("wood");
            // Console.WriteLine($"The id of my new commodity is: {myCommodity.getId()}");
            // Commodity myCommodity2 = new Commodity("food");
            // Console.WriteLine($"The id of my new commodity is: {myCommodity2.getId()}");
            // Commodity myCommodity3 = new Commodity("rice");
            // Console.WriteLine($"The id of my new commodity is: {myCommodity3.getId()}");


        }
    }
}








