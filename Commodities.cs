namespace econrpg
{
    public class Commodities
    {
        static List<Commodity> commodities = new List<Commodity>();

        static public void startCommodities(String[] namesList)
        {
            foreach (String name in namesList)
            {
                commodities.Add(new Commodity(name));
            }
        }

        static public Commodity? getOneByName(String name)
        {
            try 
            {
                Commodity foundCommodity = commodities.Find(item => item.getName() == name);
                foundCommodity.getId();
                return foundCommodity;
            }
             catch (NullReferenceException e)
            {   
                Console.WriteLine($"[Error]: Commodity with name {name} was not found");
                throw e;
            }
        }
       
        static public Commodity getOneById(int id)
        {
            try
            {
                Commodity foundCommodity = commodities.Find(item => item.getId() == id);
                foundCommodity.getId();
                return foundCommodity;
            }
            catch (NullReferenceException e)
            {   
                Console.WriteLine($"[Error]: Commodity with id {id} was not found");
                throw e;
            }
        }
    }
}