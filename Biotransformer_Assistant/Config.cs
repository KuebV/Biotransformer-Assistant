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

        public void ReloadConfig()
        {
            string loadFile = File.ReadAllText(FileData.ConfigFile);
            ConfigSettings seralize = JsonSerializer.Deserialize<ConfigSettings>(loadFile);

            OpenFiles = seralize.OpenFilesWhenPrompted;
            DebugLog = seralize.DebugLog;
        }

        public void ModifyingOpenFile(bool Value)
        {
            File.Delete(FileData.ConfigFile);

            string jsonData = string.Empty;
            bool debug = DebugLog;
            ConfigSettings settings = new ConfigSettings
            {
                OpenFilesWhenPrompted = Value,
                DebugLog = debug
            };

            jsonData = JsonSerializer.Serialize(settings);
            OpenFiles = Value;

            using (StreamWriter sw = new StreamWriter(FileData.ConfigFile))
            {
                sw.Write(jsonData);
                sw.Close();
            }
        }

        public void ModifyDebugLog(bool Value)
        {
            File.Delete(FileData.ConfigFile);

            string jsonData = string.Empty;
            bool openfile = OpenFiles;
            ConfigSettings settings = new ConfigSettings
            {
                OpenFilesWhenPrompted = openfile,
                DebugLog = Value
            };

            jsonData = JsonSerializer.Serialize(settings);
            DebugLog = Value;

            using (StreamWriter sw = new StreamWriter(FileData.ConfigFile))
            {
                sw.Write(jsonData);
                sw.Close();
            }
        }

        

        public bool OpenFiles;
        public bool DebugLog;
        public int ConfigFileLength;
    }

    public class ConfigSettings
    {
        public bool OpenFilesWhenPrompted { get; set; }
        public bool DebugLog { get; set; }
    }
}
