namespace econrpg
{
    public class Agent
    {
        private static int numberOfAgents = 0;
        private static List<Agent> agents = new List<Agent>();
        private Role role;
        private Inventory inventory;
        private int id;

        public static List<Agent> AgentsList
        {
            get { return agents; }
        }

        public Agent()
        {
            this.id = numberOfAgents;
            this.role = Role.GetRandomRole();
            inventory = new Inventory(this.id, role.GetCommodities());
            numberOfAgents++;
            agents.Add(this);
        }

        public override String ToString()
        {
            return this.id + "," + this.role.Name;
        }

        public static void printAgentsInventory()
        {
            foreach (Agent agent in agents)
            {
                agent.printInventory();
            }
        }

        public static Agent getAgentById(int agentId)
        {
            return agents.Find(x => x.id == agentId);
        }

        public static void writeAgentsStatsByRound(int round, String roundTime)
        {
            foreach (Agent agent in agents)
            {
                foreach (InventoryItem item in agent.getInventoryItems())
                {
                    StorageStatic.writeLine(new AgentStats
                    {
                        Round = round,
                        Role = agent.getCurrentRoleName(),
                        Money = agent.getMoney(),
                        AgentId = agent.Id,
                        RoundTime = roundTime,
                        CommodityId = item.getCommodityId(),
                        InventoryLevel = item.getInventoryLevel(),
                        lowerPriceBelief = item.getPriceBeliefs()[0],
                        upperPricerBelief = item.getPriceBeliefs()[1]
                    });
                }

            }
        }

        public int Id
        {
            get { return this.id; }
        }

        private List<InventoryItem> getInventoryItems()
        {
            return this.inventory.InventoryItems;
        }

        public double getMoney()
        {
            return this.inventory.Money;
        }
        public String getCurrentRoleName()
        {
            return this.role.Name;
        }
        public Role getCurrentRole()
        {
            return this.role;
        }

        public void printInventory()
        {
            Console.WriteLine("\nThis is the Agente number " + this.id);
            Console.WriteLine("The role of this agent is " + this.role);
            this.inventory.printInventory(this.role.GetRoleCommodities());
        }

        public String performProduction()
        {
            return this.role.production(this.inventory);
        }

        public List<Offer> runProductionAndOffers()
        {
            this.performProduction();
            List<Offer> items = this.inventory.generateOffers(this.role.GetRoleCommodities());
            if (items.Count == 0) Console.WriteLine("The agent " + this.id + " did not trade.");
            return items;
        }
        public void receiveOfferResult(Offer offer)
        {
            if (offer.type == "ask")
            {
                this.inventory.decreaseInventoryItemLevel(offer.commodityId, offer.filledAmount);
                this.inventory.increaseMoney(offer.wallet);
            }
            else
            {
                this.inventory.increaseInventoryItemLevel(offer.commodityId, offer.filledAmount);
                this.inventory.decreaseMoney(offer.wallet);
            }
        }

    }
}
