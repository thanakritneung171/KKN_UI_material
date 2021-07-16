using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KKN_UI.Helper
{
    public static class FileManager
    {
        private static readonly object padlock = new object();

        public static byte[] ReadFile(string filePath, string fileName)
        {
            string p = Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["ContractFilePath"], filePath, fileName);
            return File.ReadAllBytes(p);
        }

        public static void SaveFile(string filePath, string fileName, byte[] data)
        {
            var dir = Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["ContractFilePath"], filePath);
            if (!Directory.Exists(dir))
            {
                lock (padlock)
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                }
            }

            var stream = new MemoryStream(data);
            stream.Position = 0;

            using (FileStream file = new FileStream(Path.Combine(dir, fileName), FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.CopyTo(file);
            }
        }

        public static void DeleteFile(string filePath, string fileName)
        {
            string file = Path.Combine(System.Configuration.ConfigurationSettings.AppSettings["ContractFilePath"], filePath, fileName);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}