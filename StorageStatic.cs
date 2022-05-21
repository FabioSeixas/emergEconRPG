using System.Reflection;

namespace econrpg
{
    static class StorageStatic
    {
        private static String dir = Path.Combine(Directory.GetCurrentDirectory(), Globals.storageDir);
        private static String[] storageFiles = { "agents", "commodity", "trade" };

        static public void writeLine(String target, StatWritter content)
        {
            if (!storageFiles.Contains(target)) return;
            String filePath = Path.Combine(dir, target + ".txt");
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
                if (path.EndsWith(".txt"))
                {
                    File.Delete(path);
                }
            }
        }
    }

    public class StatWritter
    {
        internal static String separator = ";";
        public string writeHeader()
        {
            String lineToBeWritten = "";
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                lineToBeWritten += prop.Name + separator;
            }
            return lineToBeWritten.Remove(lineToBeWritten.Length - 1);
        }
        public string writeLine()
        {
            String lineToBeWritten = "";
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                lineToBeWritten += prop.GetValue(this) + separator;
            }
            return lineToBeWritten.Remove(lineToBeWritten.Length - 1);
        }
    }

    class TradeStats : StatWritter
    {
        public int Round { get; set; }
        public int CommodityId { get; set; }
        public double AskOfferPrice { get; set; }
        public double BidOfferPrice { get; set; }
        public double DealPrice { get; set; }
        public int TradeAmount { get; set; }
    }
    class CommodityStats : StatWritter
    {
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
}

