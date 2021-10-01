using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Biotransformer_Assistant
{
    public class FileData
    {
        public static string OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        public static string BiotransformerInput = Path.Combine(OutputDirectory, "Biotransformer_Input.txt");
        public static string SMILES_SpreadsheetKey = Path.Combine(OutputDirectory, "SMILES_SpreadsheetKey.txt");
        public static string PubChemCompounds = Path.Combine(OutputDirectory, "PubChemCompounds.txt");
        public static string SkippedSMILES = Path.Combine(OutputDirectory, "Skipped_Compounds.txt");
        public static string SkippedSMILES_BiotransformerInput = Path.Combine(OutputDirectory, "SkippedSMILES_BiotransformerInput.txt");

        public static string InputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        public static string Settings = Path.Combine(Directory.GetCurrentDirectory(), "Settings");

        public static string Translations = Path.Combine(Settings, "translations.json");
        public static string ConfigFile = Path.Combine(Settings, "config.json");

        public static string CompoundSMILES = Path.Combine(InputDirectory, "Compounds_SMILES.txt");
        public static string RawCompoundInput = Path.Combine(InputDirectory, "RawCompounds.txt");

        public static void SetupProgram()
        {
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);

            if (!Directory.Exists(InputDirectory))
                Directory.CreateDirectory(InputDirectory);

            if (!Directory.Exists(Settings))
                Directory.CreateDirectory(Settings);

            #region Output File Creation
            if (!File.Exists(BiotransformerInput))
                using (StreamWriter sw = new StreamWriter(BiotransformerInput))
                    sw.Close();

            if (!File.Exists(SMILES_SpreadsheetKey))
                using (StreamWriter sw = new StreamWriter(SMILES_SpreadsheetKey))
                    sw.Close();

            if (!File.Exists(PubChemCompounds))
                using (StreamWriter sw = new StreamWriter(PubChemCompounds))
                    sw.Close();

            if (!File.Exists(SkippedSMILES))
                using (StreamWriter sw = new StreamWriter(SkippedSMILES))
                    sw.Close();

            if (!File.Exists(SkippedSMILES_BiotransformerInput))
                using (StreamWriter sw = new StreamWriter(SkippedSMILES_BiotransformerInput))
                    sw.Close();
            #endregion

            #region Input File Creation
            if (!File.Exists(CompoundSMILES))
                using (StreamWriter sw = new StreamWriter(CompoundSMILES))
                    sw.Close();

            if (!File.Exists(RawCompoundInput))
                using (StreamWriter sw = new StreamWriter(RawCompoundInput))
                    sw.Close();

            #endregion

            if (!File.Exists(ConfigFile))
            {
                using (StreamWriter sw = new StreamWriter(ConfigFile))
                    sw.Close();
                Config cfg = new Config();
                cfg.LoadConfig();
            }

            //if (!File.Exists(Translations))
            //{
            //    TranslationData data = new TranslationData
            //    {
            //        RawCompound = "RawCompounds.txt",
            //        CompoundsForPubChem = "PubChemCompounds.txt",
            //        PubChemSMILES = "Compounds_SMILES.txt"
            //    };

            //    string seralizeData = JsonSerializer.Serialize(data);
            //    using (StreamWriter sw = new StreamWriter(Translations))
            //    {
            //        sw.Write(seralizeData);
            //        sw.Close();
            //    }
            //}
            

        }
    }
}
