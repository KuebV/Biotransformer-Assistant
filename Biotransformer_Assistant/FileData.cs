using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public static string RawCompoundInput = "RawCompounds.txt";

        public static string CompoundSMILES = "Compounds_SMILES.txt";

        public static string ConfigFile = "Config.json";

        public static void SetupProgram()
        {
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);

            string BTI = Path.Combine(Directory.GetCurrentDirectory(), BiotransformerInput);
            if (!File.Exists(BTI))
                using (StreamWriter sw = new StreamWriter(BTI))
                    sw.Close();

            string PCI = Path.Combine(Directory.GetCurrentDirectory(), CompoundSMILES);
            if (!File.Exists(PCI))
                using (StreamWriter sw = new StreamWriter(PCI))
                    sw.Close();

            string SMILES = Path.Combine(Directory.GetCurrentDirectory(), SMILES_SpreadsheetKey);
            if (!File.Exists(SMILES))
                using (StreamWriter sw = new StreamWriter(SMILES))
                    sw.Close();

            string RawCompound = Path.Combine(Directory.GetCurrentDirectory(), RawCompoundInput);
            if (!File.Exists(RawCompound))
                using (StreamWriter sw = new StreamWriter(RawCompound))
                    sw.Close();

            string pcc = Path.Combine(Directory.GetCurrentDirectory(), PubChemCompounds);
            if (!File.Exists(pcc))
                using (StreamWriter sw = new StreamWriter(pcc))
                    sw.Close();

            string skippedCompounds = Path.Combine(Directory.GetCurrentDirectory(), SkippedSMILES);
            if (!File.Exists(skippedCompounds))
                using (StreamWriter sw = new StreamWriter(skippedCompounds))
                    sw.Close();

            string skippedSMILES = Path.Combine(Directory.GetCurrentDirectory(), SkippedSMILES_BiotransformerInput);
            if (!File.Exists(skippedSMILES))
                using (StreamWriter sw = new StreamWriter(skippedSMILES))
                    sw.Close();

            string config = Path.Combine(Directory.GetCurrentDirectory(), ConfigFile);
            if (!File.Exists(config))
            {
                using (StreamWriter sw = new StreamWriter(config))
                    sw.Close();
                Config cfg = new Config();
                cfg.LoadConfig();
            }
        }
    }
}
