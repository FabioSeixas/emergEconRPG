namespace econrpg
{
    public class Agent
    {
        private Role role;
        private Inventory inventory;

        public Agent()
        {
            this.role = Role.GetRandomRole();
            inventory = new Inventory(role.GetCommodities());
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

        public void runIteration()
        {
            List<Offer> items = this.inventory.generateOffers(this.role.GetRoleCommodities());
            if (items.Count == 0) return;
        }

    }
}