
namespace econrpg
{
    class Program
    {
        static void Main(string[] args)
        {
            StorageStatic.cleanDirectory();
            String[] commoditiesNameList = { "Food", "Ore", "Tools", "Wood" };
            Commodities.startCommodities(commoditiesNameList);
            ClearingHouse clearingHouse = new ClearingHouse();

            createAgents();

            runRounds(clearingHouse);
        }

        static void createAgents()
        {
            for (int i = 0; i < Globals.numberOfAgents; i++) new Agent();
        }

        static void runRounds(ClearingHouse clearingHouse)
        {
            for (int i = 0; i < Globals.numberOfRounds; i++)
            {
                clearingHouse.Round = i;
                runRound(clearingHouse);
            }
        }

        static void runRound(ClearingHouse clearingHouse)
        {
            foreach (Agent agent in Agent.AgentsList)
            {
                List<Offer> offers = agent.runProductionAndOffers();
                clearingHouse.receiveOffers(offers);
            }
            clearingHouse.resolveOffers();
        }
    }
}








