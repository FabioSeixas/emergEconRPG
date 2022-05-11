using System.IO;
using System.Collections;
using System.Text;

namespace econrpg
{
    public class Storage
    {
        private static String dir = Directory.GetCurrentDirectory();

        private static String[] storageFiles = {"agents", "clearingHouse"};

        private Dictionary<String, FileStream> fileStreams;

        public Storage()
        {
            this.fileStreams = new Dictionary<String, FileStream>();
            foreach (String file in storageFiles)
            {
                String filePath = dir + "/storage/" + file + ".txt";
                fileStreams.Add(file,  File.Create(filePath));
            }
        }
        public void writeLine(String target, String content)
        {
            if (!storageFiles.Contains(target)) return;
            UTF8Encoding encode = new UTF8Encoding(true);
            fileStreams[target].Write(encode.GetBytes(content + "\n"), 0, encode.GetByteCount(content + "\n"));
        }

        public void closeStreams()
        {
            foreach (KeyValuePair<String, FileStream> stream in this.fileStreams)
            {
                stream.Value.Close();
            }
        }
    }
}