
namespace econrpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            String[] commoditiesNameList = {"Food", "Ore", "Tools", "Wood"};
            Commodities.startCommodities(commoditiesNameList);
            ClearingHouse clearingHouse = new ClearingHouse();

            for (int i = 0; i < Globals.numberOfAgents; i++)
            {
                Agent myAgent = new Agent();
                String roleName = myAgent.getCurrentRoleName();
                storage.writeLine("agents", myAgent.ToString());
                List<Offer> offers = myAgent.runProductionAndOffers();
                // myAgent.printInventory();
                clearingHouse.receiveOffers(offers);
            }
            clearingHouse.resolveOffers();

            // Agent.printAgentsInventory();

            storage.closeStreams();

        }
    }
}








