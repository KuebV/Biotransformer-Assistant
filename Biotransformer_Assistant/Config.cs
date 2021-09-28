using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Biotransformer_Assistant
{
    public class Config
    {
        public Config() 
        {
            if (File.Exists(FileData.ConfigFile))
                ConfigFileLength = File.ReadAllText(FileData.ConfigFile).Length;
            else
                ConfigFileLength = 0;
        }

        public void LoadConfig()
        {
            string loadFile = File.ReadAllText(FileData.ConfigFile);
            if (loadFile.Length < 1)
            {
                ConfigSettings seralize = new ConfigSettings { OpenFilesWhenPrompted = false };
                string jsonData = JsonSerializer.Serialize(seralize);
                using (StreamWriter sw = new StreamWriter(FileData.ConfigFile))
                {
                    sw.Write(jsonData);
                    sw.Close();
                }
            }
            else
            {
                ConfigSettings settings = JsonSerializer.Deserialize<ConfigSettings>(loadFile);
                OpenFiles = settings.OpenFilesWhenPrompted;
            }
        }

        public void ModifyingOpenFile(bool Value)
        {
            File.Delete(FileData.ConfigFile);

            string jsonData = string.Empty;
            if (Value)
            {
                ConfigSettings settings = new ConfigSettings
                {
                    OpenFilesWhenPrompted = true
                };

                jsonData = JsonSerializer.Serialize(settings);
                OpenFiles = true;
            }
            else
            {
                ConfigSettings settings = new ConfigSettings
                {
                    OpenFilesWhenPrompted = false
                };

                jsonData = JsonSerializer.Serialize(settings);
                OpenFiles = false;
            }

            using (StreamWriter sw = new StreamWriter(FileData.ConfigFile))
            {
                sw.Write(jsonData);
                sw.Close();
            }
        }

        

        public bool OpenFiles;
        public int ConfigFileLength;
    }

    public class ConfigSettings
    {
        public bool OpenFilesWhenPrompted { get; set; }
    }
}
