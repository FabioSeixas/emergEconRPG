using System.Reflection;

namespace econrpg
{
    static class StorageStatic
    {
        private static String dir = Path.Combine(Directory.GetCurrentDirectory(), Globals.storageDir);

        static public void writeLine(StatWritter content)
        {
            String filePath = Path.Combine(dir, content.Filename);
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(content.writeHeader());
                    sw.WriteLine(content.writeLine());
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath, true)) sw.WriteLine(content.writeLine());
            }
        }
        static public void cleanDirectory()
        {
            String[] files = Directory.GetFiles(dir);
            foreach (String path in files)
            {
                if (path.EndsWith(StatWritter.extension))
                {
                    File.Delete(path);
                }
            }
        }
    }

    abstract class StatWritter
    {
        internal static String separator = ";";
        internal static String extension = "";
        public String Filename { get { return $"{this.getFilename()}{extension}"; } }

        public abstract String getFilename();

        public string writeHeader()
        {
            String lineToBeWritten = "";
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.Name.Equals("Filename")) continue;
                lineToBeWritten += prop.Name + separator;
            }
            return lineToBeWritten.Remove(lineToBeWritten.Length - 1);
        }
        public string writeLine()
        {
            String lineToBeWritten = "";
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.Name.Equals("Filename")) continue;
                lineToBeWritten += prop.GetValue(this) + separator;
            }
            return lineToBeWritten.Remove(lineToBeWritten.Length - 1);
        }
    }

    class TradeStats : StatWritter
    {
        public override string getFilename() { return "trade"; }
        public int Round { get; set; }
        public int CommodityId { get; set; }
        public double AskOfferPrice { get; set; }
        public double BidOfferPrice { get; set; }
        public double DealPrice { get; set; }
        public int TradeAmount { get; set; }
    }
    class CommodityStats : StatWritter
    {
        public override string getFilename() { return "commodity"; }
        public int Round { get; set; }
        public int CommodityId { get; set; }
        public double SDRatio { get; set; }
        public int AskTotalAmount { get; set; }
        public int BidTotalAmount { get; set; }
        public int totalAmountTraded { get; set; }
        public double askPriceAvg { get; set; }
        public double bidPriceAvg { get; set; }
        public double dealPriceAvg { get; set; }
    }
    class AgentStats : StatWritter
    {
        public override string getFilename() { return "agents"; }
        public int Round { get; set; }
        public String RoundTime { get; set; }
        public int AgentId { get; set; }
        public String Role { get; set; }
        public double Money { get; set; }
        public int CommodityId { get; set; }
        public int InventoryLevel { get; set; }
        public double lowerPriceBelief { get; set; }
        public double upperPricerBelief { get; set; }
    }
}

