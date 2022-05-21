
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

            List<Agent> agents = createAgents();

            runRounds(agents, clearingHouse);
        }

        static List<Agent> createAgents()
        {
            List<Agent> agents = new List<Agent>();
            for (int i = 0; i < Globals.numberOfAgents; i++)
            {
                Agent newAgent = new Agent();
                agents.Add(newAgent);
            }
            return agents;
        }

        static void runRounds(List<Agent> agents, ClearingHouse clearingHouse)
        {
            for (int i = 0; i < Globals.numberOfRounds; i++)
            {
                clearingHouse.Round = i;
                runRound(agents, clearingHouse);
            }
        }

        static void runRound(List<Agent> agents, ClearingHouse clearingHouse)
        {
            foreach (Agent agent in agents)
            {
                List<Offer> offers = agent.runProductionAndOffers();
                clearingHouse.receiveOffers(offers);
            }
            clearingHouse.resolveOffers();
        }
    }
}








