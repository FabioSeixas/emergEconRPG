
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
            for (int round = 0; round < Globals.numberOfRounds; round++)
            {
                clearingHouse.Round++;
                Agent.writeAgentsStatsByRound(clearingHouse.Round, "start");
                runRound(clearingHouse);
                Agent.writeAgentsStatsByRound(clearingHouse.Round, "end");
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








