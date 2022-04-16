namespace econrpg
{
    public class Agent
    {
        private Role role;
        private Inventory inventory;
        public Agent()
        {
            this.role = Role.GetRandomRole();
            inventory = new Inventory();
            inventory.startInventory(role.GetCommodities());
        }

        public String getCurrentRoleName()
        {
            return role.GetName();
        }
        public Role getCurrentRole()
        {
            return this.role;
        }

        public void printInventory()
        {
            this.inventory.printInventory();
        }

        public String performProduction()
        {
            return this.role.production(this.inventory);
        }

    }
}