using System;
using System.Collections.Generic;
using System.IO;

namespace Biotransformer
{
    class Program
    {
        static void Main(string[] args)
        {

            bool menu = true;
            while (menu)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Paste compound individually (Good for quick and easy conversions)");
                Console.WriteLine("2. Load Compounds via File (If you got a lot of compounds you want done quickly)");
                Console.WriteLine("3. Setup Program (Only use if this is your first time)");

                int index;
                if (int.TryParse(Console.ReadLine(), out index))
                {
                    switch (index)
                    {
                        case 1:
                            menu = false;
                            PasteIndivdualCompounds();
                            break;
                        case 2:
                            menu = false;
                            LoadCompounds();
                            break;
                        case 3:
                            SetupProgram();
                            Log.Info("Setup Complete, Press \'Enter\' to Exit the Program");
                            Console.ReadLine();
                            Environment.Exit(0);
                            break;
                    }
                }
            }

            Log.Info("Press \'Enter\' to continue into the next section");
            Console.ReadLine();

            Console.Clear();

            Log.Info("Instructions for next section :\n");
            Console.WriteLine(@"Go to PubChem Identifier Exchange Service
Set Input ID List to “Synonyms”, and paste the previous output into the textbox below.
Set Operator Type to “Same CID”.
Set Output IDs to “SMILES”.
Set OutPut Method to “Two column file showing each input-output correspondence”
Set Compression to “No Compression”
Press “Submit Job” at the bottom left.
Once the text file has loaded, copy and paste the resulting data into the file titled: PubChemOutput
");

            Log.Info("When you have pasted the data into the PubChemOutput File, Press \'Enter\' to start the parsing process");
            Console.ReadLine();
            PostPubChem.LoadChemFile();

            Log.Success("Done!");
            Log.Info("Press \'Enter\' to continue into the final step");
            Console.ReadLine();

            Console.Clear();
            Log.Info("Enter metabolism type: ");
            string metabolismType = Console.ReadLine();

            Log.Info("Enter file type: ");
            string fileType = Console.ReadLine();

            Log.Info("Enter list name: ");
            string listName = Console.ReadLine();

            Console.Clear();

            int i = 0;

            Dictionary<string, string> SMILESKey = new Dictionary<string, string>();
            List<string> BioTransformerCMDS = new List<string>();
            foreach (var value in PostPubChem.PubChemSMILES)
            {
                i = i + 1;
                string name = ($"{listName}" + ".cmpnd" + i + "." + fileType);
                string bio = "java - jar \"biotransformer-1.1.5 (1).jar\" -k pred -b " + metabolismType + " -ismi \"" + value.Value + "\" -ocsv " + name + " -s 1";
                Console.WriteLine(bio);

                BioTransformerCMDS.Add(bio);
                SMILESKey.Add(value.Key, value.Value);
            }

            Log.Info("\n\nProcess Completed. Press \'Enter\' to create a Biotransformer Input & Key File (This will overwrite any existing file)");
            Log.Info("If you are done, close the program");
            Console.ReadLine();

            string finalOutput = Path.Combine(Directory.GetCurrentDirectory(), "BioTransformerInput.txt");
            File.Delete(finalOutput);

            using (StreamWriter sw = new StreamWriter(finalOutput))
            {
                sw.WriteLine("BioTransformer Input:");
                sw.WriteLine("------------------------------------------------------------------------------------------");
                foreach (var cmd in BioTransformerCMDS)
                    sw.WriteLine(cmd);

                sw.WriteLine("------------------------------------------------------------------------------------------");

                sw.WriteLine("SMILES Key");

                int x = 0;
                foreach (var val in PostPubChem.PubChemSMILES)
                {
                    x = x + 1;
                    sw.WriteLine($"[{x}] - {val.Key} | {val.Value}");
                }
                sw.Close();
            }


        }

        public static List<string> Compounds = new List<string>();
        public static List<string> ParsedCompounds = new List<string>();

        public static IEnumerable<string> RawCompoundsFromFile;
        public static List<string> ParsedCompoundsFromFile = new List<string>();

        public static void LoadCompounds()
        {
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
            if (!File.Exists(localPath))
            {
                Log.Error("input.txt does not exist! Are you correctly following the documentation?");
                return;
            }

            RawCompoundsFromFile = File.ReadLines(localPath);

            Log.Success("Loaded Compounds from input.txt");
            Log.Info("Parsing Compounds...");

            foreach (string rawcompound in RawCompoundsFromFile)
                ParsedCompoundsFromFile.Add(PrePubChem.ParseCompounds(rawcompound));

            Log.Success("Done!");
            Log.Info("Here are the parsed compounds: \n\n");
            foreach (string comp in ParsedCompoundsFromFile)
                Console.WriteLine(comp);


        }

        public static void PasteIndivdualCompounds()
        {
            Console.Clear();
            Log.Info("Paste the Compounds you want converted, when you are done, type \'STOP\'");

            bool inputting = true;
            while (inputting)
            {
                Log.Info("Enter the Compound:\n");

                string compound = Console.ReadLine();
                if (compound.Contains("STOP"))
                    inputting = false;
                else
                    Compounds.Add(compound);
            }

            Log.Info("Parsing Compound Strings...");
            foreach (string compound in Compounds)
                ParsedCompounds.Add(PrePubChem.ParseCompounds(compound));

            Log.Success("Done!");
            Log.Info("Here are the parsed compounds:\n\n");
            foreach (string parsed in ParsedCompounds)
                Console.WriteLine(parsed);


        }

        public static void SetupProgram()
        {
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
            if (!File.Exists(localPath))
            {
                using (StreamWriter sw = new StreamWriter(localPath))
                {
                    sw.Close();
                }
            }

            string pubchemoutput = Path.Combine(Directory.GetCurrentDirectory(), "PubChemOutput.txt");
            if (!File.Exists(pubchemoutput))
            {
                using (StreamWriter sw = new StreamWriter(pubchemoutput))
                {
                    sw.Close();
                }
            }

            string finalOutput = Path.Combine(Directory.GetCurrentDirectory(), "BioTransformerInput.txt");
            if (!File.Exists(finalOutput))
            {
                using (StreamWriter sw = new StreamWriter(finalOutput))
                {
                    sw.Close();
                }
            }
        }
    }
}
