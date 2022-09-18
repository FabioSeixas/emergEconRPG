using System.Text.Json;

namespace econrpg
{

    public class RoleProductionRules
    {
        public Globals.Roles RoleName { get; set; }
        public List<ProductionRule> rulesByOutputAmount { get; set; }
        public static List<RoleProductionRules> productionRulesList = new List<RoleProductionRules>();
        static public RoleProductionRules GetProductionRuleByRoleName(Globals.Roles roleName)
        {
            return productionRulesList.Find(x => roleName.Equals(x.RoleName));
        }
        public RoleProductionRules(Globals.Roles roleName, List<ProductionRule> rules)
        {
            this.RoleName = roleName;
            this.rulesByOutputAmount = rules;
            productionRulesList.Add(this);
        }

        public void print()
        {
            Console.WriteLine("These are the Production rules for: " + this.RoleName);
            foreach (ProductionRule productionRule in this.rulesByOutputAmount)
            {
                productionRule.print();
            }
        }

        public ProductionRule readyToProduce (Inventory inventory)
        {
            foreach (ProductionRule prodRule in this.rulesByOutputAmount)
            {
                bool readyForCurrentOutput = true;
                foreach (RecipeItem productionRecipeItem in prodRule.Resources)
                {
                    int currentLevel = inventory.getInventoryItemLevel(productionRecipeItem.CommodityId);
                    readyForCurrentOutput = currentLevel >= productionRecipeItem.Amount;
                    if (!readyForCurrentOutput) break;
                }

                if (readyForCurrentOutput) return prodRule;
            }
            ProductionRule anyProductionRule = this.rulesByOutputAmount.First();
            return new ProductionRule(anyProductionRule.OutputId, 0, new List<RecipeItem>());
        }

    }
    public class ProductionRule
    {
        public int OutputId { get; set; }
        public int OutputAmount { get; set; }
        public List<RecipeItem> Resources { get; set; }
        internal ProductionRule(int id, int amount, List<RecipeItem> resources)
        {
            this.OutputAmount = amount;
            this.OutputId = id;
            this.Resources = resources;
        }
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
    public struct ProductionRuleJSONOutput
    {
        public int OutputId { get; set; }
        public int OutputAmount { get; set; }
        public List<RecipeItem> Resources { get; set; }
    }

    public struct RecipeItem
    {
        public int CommodityId { get; set; }
        public int Amount { get; set; }
    }

    public class RoleProductionRulesJSONOutput
    {
        public String RoleName { get; set; }
        public List<ProductionRuleJSONOutput> rulesByOutputAmount { get; set; }
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
                List<ProductionRule> productionRules = new List<ProductionRule>();
                foreach (ProductionRuleJSONOutput rule in role.rulesByOutputAmount)
                {
                    productionRules.Add(new ProductionRule(rule.OutputId, rule.OutputAmount, rule.Resources));
                }
                new RoleProductionRules(Globals.getRoleByName(role.RoleName), productionRules);
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
