using System.Text;
using System.Reflection;

namespace econrpg
{
    static class StorageStatic
    {
        private static String dir = Path.Combine(Directory.GetCurrentDirectory(), Globals.storageDir);
        private static String[] storageFiles = {"agents", "commodity", "trade"};

        static public void writeLine(String target, Stats content)
        {
            if (!storageFiles.Contains(target)) return;
            String filePath = Path.Combine(dir, target + ".txt");
            UTF8Encoding encode = new UTF8Encoding(true);
            if (!File.Exists(filePath))
            { 
                using (StreamWriter sw = File.CreateText(filePath)){
                    sw.WriteLine(content.writeHeader());
                    sw.WriteLine(content.writeLine());
                } 
            } else {
                using (StreamWriter sw = new StreamWriter(filePath, true)) sw.WriteLine(content.writeLine());
            }
        }
    }

    public class Stats
    {
        internal static String separator = ";";
        // public abstract String writeHeader();
        // public abstract String writeLine();
        public string writeHeader()
        {
            String lineToBeWritten = "";
            foreach(PropertyInfo prop in this.GetType().GetProperties())
            {
                lineToBeWritten += prop.Name + separator;
            }
            return lineToBeWritten.Remove(lineToBeWritten.Length - 1);
        }
        public string writeLine()
        {
            String lineToBeWritten = "";
            foreach(PropertyInfo prop in this.GetType().GetProperties())
            {
                lineToBeWritten += prop.GetValue(this) + separator;
            }
            return lineToBeWritten.Remove(lineToBeWritten.Length - 1);
        }
    }

    class TradeStats : Stats
    {
        public int Round { get; set; }
        public int CommodityId { get; set; }
        public double AskOfferPrice { get; set; }
        public double BidOfferPrice { get; set; }
        public double DealPrice { get; set; }
        public int TradeAmount { get; set; }
    }
    class CommodityStats : Stats
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

