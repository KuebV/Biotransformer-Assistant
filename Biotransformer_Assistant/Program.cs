using System;
using System.Collections.Generic;
using System.IO;

namespace Biotransformer_Assistant
{
    class Program
    {

        private static void Exit(string exitMessage)
        {
            Log.Error(exitMessage);
            Log.Error("Press \"Enter\" to exit the program");
            Console.ReadLine();
            Environment.Exit(0);
        }
        static void Main(string[] args)
        {
            Log.WriteLine("BioTransformer-Assistant", ConsoleColor.Cyan);
            Log.WriteLine("By: Robert Thompson (https://github.com/KuebV)\n", ConsoleColor.Red);

            Log.WriteLine("Please enter 1, 2, or 3 to select one of the options below:", ConsoleColor.Cyan);
            Log.WriteLine("----------------------------------------------------------", ConsoleColor.Blue);
            Log.WriteLine("[1] Load compounds from file	    (primary use)", ConsoleColor.Cyan);
            Log.WriteLine("[2] Parse skipped compounds 	    (only use if you’ve completed option [1])", ConsoleColor.Cyan);
            Log.WriteLine("[3] Setup Program 		    (please use if this is your first time)", ConsoleColor.Cyan);
            Log.WriteLine("----------------------------------------------------------", ConsoleColor.Blue);

            // This is where it all begins, with the Menu
            bool menu = true;
            while (menu)
            {
                Log.Write("Input: ", ConsoleColor.Yellow);

                // Define the Index Integer, it will attempt to parse the input as an integer, if it is unable to, display error
                int index;
                if (int.TryParse(Console.ReadLine(), out index))
                {
                    switch (index)
                    {
                        case 1:
                            menu = false;
                            LoadCompounds();
                            break;
                        case 2:
                            menu = false;
                            break;
                        case 3:
                            FileData.SetupProgram();
                            Log.Info("Setup Complete, Press \'Enter\' to continue");
                            Console.ReadLine();
                            break;
                        default:
                            Log.Error("Invalid Input");
                            break;
                    }
                }
                else
                    Log.Error("Invalid Input : Must be type Integer\n");
            }
            Console.Clear();

            Log.Info("Instructions for next section :\n");
            Log.WriteLine("[1] Go to “PubChem Identifier Exchange Service” in any web browser\n" +
                            "\tAvailable at: https://pubchem.ncbi.nlm.nih.gov/idexchange/idexchange.cgi");
            Log.WriteLine("[2] Set “Input ID List” to “Synonyms”");
            Log.WriteLine("[3] Select “Choose File“ and select the file in the Output Folder, labeled “PubChemCompounds“");
            Log.WriteLine("[4] Set “Operator Type” to “Same CID”");
            Log.WriteLine("[5] Set the Output IDs to “SMILES“");
            Log.WriteLine("[6] Set “Output Method” to “Two column file showing each input-output correspondence”");
            Log.WriteLine("[7] Set “Compression” to “No Compression”");
            Log.WriteLine("[8] Press “Submit Job” at the bottom left");
            Log.WriteLine("[9] It may take a few moments for PubChem to process the data. Once the text has loaded, copy and paste this output into the file called “Compound_SMILES“. Save and close the file");
            Log.Warning("PubChem may not be able to convert every compound into a SMILES format.  As such, those compounds will be skipped in future output and will have to be done manually.");

            Log.Info("When you have pasted the data into the PubChemOutput File, Press \'Enter\' to start the parsing process");
            Console.ReadLine();
            Log.Warning("This may take several seconds, please be patient!");

            PostPubChem.LoadChemFile();

            Log.Success("Done!");
            Log.Info("Press \'Enter\' to continue into the final step");
            Console.ReadLine();

            // Ask for the types that BioTransformer needs, if nothing is inputted, just default
            Console.Clear();
            Log.Info("Enter metabolism type (default: allHuman) : ");
            string metabolismType = Console.ReadLine();
            if (string.IsNullOrEmpty(metabolismType))
                metabolismType = "allHuman";

            Log.Info("Enter file type (default: csv) : ");
            string fileType = Console.ReadLine();
            if (string.IsNullOrEmpty(fileType))
                fileType = "csv";

            Log.Info("Enter list name (default: list1) : ");
            string listName = Console.ReadLine();
            if (string.IsNullOrEmpty(listName))
                listName = "list1";

            Log.WriteLine("---------------------------------------------------------------------------------------------------", ConsoleColor.Blue);
            Log.Warning("All data stored in the files “BioTransformer_Input” and “SMILES_SpreadsheetKey” will be overwritten.");
            Log.Info("Press “Enter” to continue");
            Console.ReadLine();

            Console.Clear();

            // Logic Almighty
            // I don't care to explain how this all works, but the point being, it works, and it works well
            List<string> BioTransformerCMDS = new List<string>();

            List<string> SkippedSMILES = new List<string>();

            Dictionary<string, string> LocalSMILES = PostPubChem.PubChemSMILES;
            int i = 0;
            foreach (var value in PostPubChem.PubChemSMILES)
            {
                i = i + 1;
                if (!string.IsNullOrEmpty(value.Value))
                {
                    // Welcome Back To: Double check your strings before publishing incase a theres a random space preventing the program from running
                    string name = ($"{listName}" + ".cmpnd" + i + "." + fileType);
                    string bio = "java -jar \"biotransformer-1.1.5 (1).jar\" -k pred -b " + metabolismType + " -ismi \"" + value.Value + "\" -o" + fileType + " " + name + " -s 1";

                    BioTransformerCMDS.Add(bio);
                }
                else
                {
                    string compoundFormatted = $"[{i}] {value.Key}";
                    SkippedSMILES.Add(compoundFormatted);
                }
            }

            Log.Success("Done!");

            Log.Info("\n\nProcess Completed. Press \'Enter\' to create a Biotransformer Input & Key File (This will overwrite any existing file)");
            Console.ReadLine();

            string finalOutput = Path.Combine(Directory.GetCurrentDirectory(), "BioTransformerInput.txt");
            File.Delete(FileData.BiotransformerInput);

            //Format the BioTransformerInput
            using (StreamWriter sw = new StreamWriter(FileData.BiotransformerInput))
            {
                sw.WriteLine("BioTransformer Input:");
                sw.WriteLine("------------------------------------------------------------------------------------------");
                foreach (var cmd in BioTransformerCMDS)
                    sw.WriteLine(cmd);

                sw.WriteLine("------------------------------------------------------------------------------------------");
                sw.Close();
            }

            using (StreamWriter sw = new StreamWriter(FileData.SMILES_SpreadsheetKey))
            {
                foreach (var val in PostPubChem.PubChemSMILES)
                {
                    if (val.Key != null)
                        sw.WriteLine($"{val.Value}");
                    else
                        sw.WriteLine("");
                }
                sw.Close();
            }

            using (StreamWriter sw = new StreamWriter(FileData.SkippedSMILES))
            {
                foreach (string skippedCompound in SkippedSMILES)
                {
                    sw.WriteLine(skippedCompound);
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
            Log.Info("\n[1] Please paste unparsed compounds, one per line, into the text document “RawCompounds”\n" +
                "[2] Press [CONTROL]+S to save, then close the file\n\n" +
                "When finished, press “Enter” to continue:");

            Console.ReadLine();
            string RawCompounds = Path.Combine(Directory.GetCurrentDirectory(), FileData.RawCompoundInput);

            string CheckFileComponents = File.ReadAllText(RawCompounds);
            if (CheckFileComponents.Length < 1)
                Exit("RawCompounds is Empty!");

            RawCompoundsFromFile = File.ReadLines(RawCompounds);

            Log.Success("Loaded Compounds from " + FileData.RawCompoundInput);
            Log.Info("Parsing Compounds...");

            foreach (string rawcompound in RawCompoundsFromFile)
                ParsedCompoundsFromFile.Add(PrePubChem.ParseCompounds(rawcompound));

            Log.Success("Done!");
            Log.Success("The Parsed Compounds can be found at : " + FileData.PubChemCompounds);

            File.Delete(FileData.PubChemCompounds);
            using (StreamWriter SW = new StreamWriter(FileData.PubChemCompounds))
            {
                foreach (string compounds in ParsedCompoundsFromFile)
                {
                    SW.WriteLine(compounds);
                }
            }

            // When the inital compounds are parsed, run this function
            Log.Info("Press \'Enter\' to continue into the next section");
            Console.ReadLine();


        }
    }
}
