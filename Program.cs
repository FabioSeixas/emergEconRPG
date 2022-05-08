
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
                String roleName = myAgent.getCurrentRoleName();
                myAgent.printInventory();
                List<Offer> offers = myAgent.runProductionAndOffers();
                myAgent.printInventory();
                clearingHouse.receiveOffers(offers);
            }
            clearingHouse.resolveOffers();

            Agent.printAgentsInventory();
        }
    }
}








