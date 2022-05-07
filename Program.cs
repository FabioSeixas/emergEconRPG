
namespace econrpg
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] commoditiesNameList = {"Food", "Ore", "Tools", "Wood"};
            Commodities.startCommodities(commoditiesNameList);
            ClearingHouse clearingHouse = new ClearingHouse();

            for (int i = 0; i < Globals.numberOfAgents; i++)
            {
                Agent myAgent = new Agent();
                Console.WriteLine("\nThis is the Agente number " + myAgent.Id);
                String roleName = myAgent.getCurrentRoleName();
                Console.WriteLine("The role of my agent is " + roleName);
                myAgent.printInventory();
                List<Offer> offers = myAgent.runProductionAndOffers();
                clearingHouse.receiveOffers(offers);
            }

            clearingHouse.resolveOffers();

            foreach (Book book in clearingHouse.bookList)
            {
                book.printOffers();
            }

            
        }
    }
}








