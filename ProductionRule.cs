using System.Text.Json;

namespace econrpg
{
    enum roles
    {
        Miner,
        Farmer,
        Woodcutter,
        Blacksmith
    }

    public class RoleProductionRules
    {
        public String RoleName { get; set; }
        public List<ProductionRule> rulesByOutputAmount { get; set; }
        public static List<RoleProductionRules> roles = new List<RoleProductionRules>();

        public RoleProductionRules(String roleName, List<ProductionRule> rules)
        {
            this.RoleName = roleName;
            this.rulesByOutputAmount = rules;
            roles.Add(this);
        }

        public void print()
        {
            Console.WriteLine("This are the Production rules for: " + this.RoleName);
            foreach (ProductionRule productionRule in this.rulesByOutputAmount)
            {
                productionRule.print();
            }
        }

    }

    public class ProductionRule
    {
        public int OutputAmount { get; set; }
        public List<RecipeItem> Resources { get; set; }
        public void print()
        {
            Console.WriteLine("For an output Amount of: " + this.OutputAmount);
            Console.WriteLine("Are necessary...");

            foreach (RecipeItem recipeItem in this.Resources)
            {
                String inputName = Commodities.getOneById(recipeItem.CommodityId).getName();
                Console.WriteLine(recipeItem.Amount + " of " + inputName);
            }
        }
    }

    public struct RecipeItem
    {
        public int CommodityId { get; set; }
        public int Amount { get; set; }
    }

    public class RoleProductionRulesJSONOutput
    {
        public String RoleName { get; set; }
        public List<ProductionRule> rulesByOutputAmount { get; set; }
    }


    static class RulesReader
    {
        private static String dir = Path.Combine(Directory.GetCurrentDirectory(), Globals.rulesDir);

        static private void readOneRuleFile(String path)
        {
            string json = File.ReadAllText(path);
            RoleProductionRulesJSONOutput role = JsonSerializer.Deserialize<RoleProductionRulesJSONOutput>(json);
            if (role != null)
            {
                new RoleProductionRules(role.RoleName, role.rulesByOutputAmount);
            }
        }

        static public void readRules()
        {
            String[] files = Directory.GetFiles(dir);
            foreach (String path in files)
            {
                readOneRuleFile(path);
            }
        }

    }

}
