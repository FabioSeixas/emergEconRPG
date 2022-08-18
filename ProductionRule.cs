using System.Collections;
using System.Text;
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

    public class ProductionRule
    {
        public int OutputAmount { get; set; }
        public List<RecipeItem> Resources {get; set; }
        public String RoleName {get; set; }
        public void print()
        {

                Console.WriteLine("Output Amount: " + this.OutputAmount);
                Console.WriteLine("Role Name: " + this.RoleName);
            foreach (RecipeItem recipeItem in this.Resources)
            {
                Console.WriteLine("CommodityId: " + recipeItem.CommodityId);
                Console.WriteLine("Amount: :" + recipeItem.Amount);
            } 
        }

        // public ProductionRule(String roleName)
        // {
        //     this.RoleName = roleName;
        //     // this.Resources = new List<RecipeItem>();
        //     // ProductionRules.Add(this);
        // }

        // public void print()
        // {
        //     foreach (KeyValuePair<int, Recipe> recipe in this.recipes)
        //     {
        //         Console.WriteLine("\n One Recipe");
        //         recipe.Value.printRecipes();
        //     }
        // }

    }

    // public class Recipe
    // {
    //     List<RecipeItem> recipeItems;

    //     public Recipe()
    //     {
    //         this.recipeItems = new List<RecipeItem>();
    //     }

    //     public void addRecipeItem(int commodityId, int inputAmount)
    //     {
    //         this.recipeItems.Add(new RecipeItem(commodityId, inputAmount));
    //     }

    //     public void printRecipes()
    //     {
    //         foreach (RecipeItem recipeItem in this.recipeItems)
    //         {
    //             Console.WriteLine("One recipe Item:");
    //             Console.WriteLine(recipeItem.inputCommodityId);
    //             Console.WriteLine(recipeItem.inputCommodityAmount);
    //         }
    //     }

    // }

    public struct RecipeItem
    {
            public int CommodityId { get; set; }
            public int Amount { get; set; }
        // public RecipeItem (int commodityId, int inputAmount)
        // {
        //     inputCommodityAmount = commodityId;
        //     inputCommodityAmount = inputAmount;
        // }
    }

    static class RulesReader
    {
        private static String dir = Path.Combine(Directory.GetCurrentDirectory(), Globals.rulesDir);
        private static Dictionary<string, int> indexPositions = new Dictionary<string, int>()
        {
          {"outputAmount", 0},
          {"commodityId", 3},
          {"inputAmount", 7},
        };

        static private void readOneRuleFile(String path)
        {
            string json = File.ReadAllText(path);
            Console.WriteLine(json);
            ProductionRule production = JsonSerializer.Deserialize<ProductionRule>(json);
            production.print();
            // ProductionRule rule = new ProductionRule(Path.GetFileName(path));
            // Console.WriteLine("\n" + rule.roleName);
            // using (StreamReader sr = new StreamReader(path, Encoding.Default))
            // {
            //     string line;
            //     string outputAmount;
            //     string commodityId;

            //     while ((line = sr.ReadLine()) != null)
            //     {
            //         if (line == "") continue;

            //         Console.WriteLine(line);
            //         Dictionary<string, int> lineValue = new Dictionary<string, int>();

            //         string[] keyValuePair = line.Split(":");

            //         if (keyValuePair[0].Trim() == "outputAmount") {
            //             outputAmount = keyValuePair[1];
            //             continue;
            //         }
            //         if (keyValuePair[0].Trim() == "commodityId") {
            //             commodityId = keyValuePair[1];
            //             continue;
            //         }
            //         if (keyValuePair[0].Trim() == "inputAmount") {

            //         rule.addRecipeItem(
            //             Convert.ToInt32(outputAmount),
            //             commodityId,
            //             keyValuePair[1]
            //             );

            //         }

                   
            //     }
            // }
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
