using System.Text;

namespace econrpg
{
    static class StorageStatic
    {
        private static String dir = Path.Combine(Directory.GetCurrentDirectory(), Globals.storageDir);
        private static String[] storageFiles = {"agents", "clearingHouse", "trade"};

        static public void writeLine(String target, String content)
        {
            if (!storageFiles.Contains(target)) return;
            String filePath = Path.Combine(dir, target + ".txt");
            UTF8Encoding encode = new UTF8Encoding(true);
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath)) sw.WriteLine(content);
            } else {
                using (StreamWriter sw = new StreamWriter(filePath, true)) sw.WriteLine(content);
            }
        }
    }
}