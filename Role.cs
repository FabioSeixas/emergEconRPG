namespace econrpg
{
    public abstract class Role
    {
        public static Role GetRandomRole()
        {
            Globals.Roles randomRole = Globals.GetRandomRole();
            switch (randomRole.ToString())
            {
                case "Miner":
                    return new Miner();
                case "Farmer":
                    return new Farmer();
                case "Woodcutter":
                    return new Woodcutter();
                case "Blacksmith":
                    return new Blacksmith();
                default:
                    return new Miner();
            }

        }
        public abstract String Name { get; }
        protected String name;
        public abstract ProductionRule production(Inventory inventory);

        public abstract RoleProductionRules getProductionRules();

    }

    public class Miner : Role
    {
        public static RoleProductionRules productionRule = RoleProductionRules.GetProductionRuleByRoleName(
            Globals.getRoleByName("Miner")
        );
        public override RoleProductionRules getProductionRules()
        {
            return productionRule;
        }
        public Miner()
        {
            this.name = "Miner";
        }

        public override String Name { get { return this.name; } }

        public override ProductionRule production(Inventory inventory)
        {
            ProductionRule readyProductionRule = productionRule.readyToProduce(inventory);
            if (readyProductionRule.OutputAmount > 0) 
            {
                inventory.adjustInventoryLevelsAccordingToProductionRule(readyProductionRule);
            } 
            return readyProductionRule;
        }
    }

    sealed class Farmer : Role
    {
        public static RoleProductionRules productionRule = RoleProductionRules.GetProductionRuleByRoleName(
            Globals.getRoleByName("Farmer")
        );
        public override RoleProductionRules getProductionRules()
        {
            return productionRule;
        }
        public Farmer()
        {
            this.name = "Farmer";
        }
        public override String Name { get { return this.name; } }

        public override ProductionRule production(Inventory inventory)
        {
            ProductionRule readyProductionRule = productionRule.readyToProduce(inventory);
            if (readyProductionRule.OutputAmount > 0) 
            {
                inventory.adjustInventoryLevelsAccordingToProductionRule(readyProductionRule);
            } 
            return readyProductionRule;
        }
    }

    sealed class Woodcutter : Role
    {
        public static RoleProductionRules productionRule = RoleProductionRules.GetProductionRuleByRoleName(
            Globals.getRoleByName("Woodcutter")
        );
        public override RoleProductionRules getProductionRules()
        {
            return productionRule;
        }
        public Woodcutter()
        {
            this.name = "Woodcutter";
        }
        public override String Name { get { return this.name; } }

        public override ProductionRule production(Inventory inventory)
        {
            ProductionRule readyProductionRule = productionRule.readyToProduce(inventory);
            if (readyProductionRule.OutputAmount > 0) 
            {
                inventory.adjustInventoryLevelsAccordingToProductionRule(readyProductionRule);
            } 
            return readyProductionRule;
        }
    }

    sealed class Blacksmith : Role
    {
        public static RoleProductionRules productionRule = RoleProductionRules.GetProductionRuleByRoleName(
            Globals.getRoleByName("Blacksmith")
        );
        public override RoleProductionRules getProductionRules()
        {
            return productionRule;
        }
        public Blacksmith()
        {
            this.name = "Blacksmith";
        }
        public override String Name { get { return this.name; } }

         public override ProductionRule production(Inventory inventory)
        {
            ProductionRule readyProductionRule = productionRule.readyToProduce(inventory);
            if (readyProductionRule.OutputAmount > 0) 
            {
                inventory.adjustInventoryLevelsAccordingToProductionRule(readyProductionRule);
            } 
            return readyProductionRule;
        }
    }
}
