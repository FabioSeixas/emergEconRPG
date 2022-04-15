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
            inventory.startInventory(role.getRoleCommodities());
        }

        public String getCurrentRoleName()
        {
            return role.GetName();
        }
        public Role getCurrentRole()
        {
            return role;
        }

        public void printInventory()
        {
            this.inventory.printInventory();
        }

    }
}