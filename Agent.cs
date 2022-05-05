namespace econrpg
{
    public class Agent
    {
        static int numberOfAgents = 0;
        private Role role;
        private Inventory inventory;

        private int id;

        public Agent()
        {
            this.id = numberOfAgents;
            this.role = Role.GetRandomRole();
            inventory = new Inventory(this.id, role.GetCommodities());
            numberOfAgents++;
        }

        public int Id
        {
            get { return this.id; }
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

    }
}