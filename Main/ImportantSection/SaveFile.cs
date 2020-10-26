using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ImportantSection
{
    public class SaveFile
    {
        public static SaveFile SFile;
        public SaveFile()
        {
            Logger.debug("SaveFile begin...");
            SFile = this;
            CFile = new FileInfo(CDirectory.FullName + "/CFile.json");
            LoadConfig();
            Logger.debug("Success!");
        }
        public FileInfo CFile
        {
            get;
            private set;
        }

        public DirectoryInfo CDirectory = new DirectoryInfo(Environment.CurrentDirectory + "/AceSectionData");

        public void LoadConfig()
        {

        }

        public void SaveConfig() //Saving the data/config
        {
            
   
        }
    }
}
