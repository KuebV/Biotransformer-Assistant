using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Biotransformer_Assistant
{
    public class Menu
    {
        public static void StartMenu()
        {
            Log.WriteLine("BioTransformer-Assistant", ConsoleColor.Cyan);
            Log.WriteLine("By: Robert Thompson (https://github.com/KuebV)\n", ConsoleColor.Red);

            Log.WriteLine("Please enter 1, 2, or 3 to select one of the options below:", ConsoleColor.Cyan);
            Log.WriteLine("----------------------------------------------------------", ConsoleColor.Blue);
            Log.WriteLine("[1] Load compounds from file	    (primary use)", ConsoleColor.Cyan);
            Log.WriteLine("[2] Parse skipped compounds 	    (only use if you’ve completed option [1])", ConsoleColor.Cyan);
            Log.WriteLine("[3] Setup Program 		    (please use if this is your first time)", ConsoleColor.Cyan);

            Config config = new Config();

            if (config.ConfigFileLength >= 1)
            {
                Log.WriteLine("[4] Configuration                   (Configure the Settings for BTA)", ConsoleColor.Cyan);
            }

            Log.WriteLine("----------------------------------------------------------", ConsoleColor.Blue);

            bool atStartMenu = true;
            while (atStartMenu)
            {
                Log.Write("Input: ", ConsoleColor.Yellow);

                // Define the Index Integer, it will attempt to parse the input as an integer, if it is unable to, display error
                int index;
                if (int.TryParse(Console.ReadLine(), out index))
                {
                    switch (index)
                    {
                        case 1:
                            LoadCompounds();
                            atStartMenu = false;
                            break;
                        case 2:
                            ParseSkippedCompounds();
                            atStartMenu = false;
                            break;
                        case 3:
                            FileData.SetupProgram();
                            Log.Info("Setup Complete, Press \'Enter\' to continue");
                            Console.ReadLine();
                            Console.Clear();
                            StartMenu();
                            break;
                        case 4:
                            Settings();
                            break;
                        default:
                            Log.Error("Invalid Input");
                            break;
                    }
                }
                else
                    Log.Error("Invalid Input : Must be type Integer\n");
            }
        }

        // Int              String
        // Compound Number, SMILE Format
        internal static Dictionary<int, string> SkippedCompoundsDictionary = new Dictionary<int, string>();

        public static void ParseSkippedCompounds()
        {
            IEnumerable<string> FileSkippedCompounds;
            FileSkippedCompounds = File.ReadAllLines(FileData.SkippedSMILES);
            foreach (var data in FileSkippedCompounds)
            {
                if (data.Contains("|||"))
                {
                    string[] CompoundElementSplit = data.Split("|||");
                    int NumberCompound = Convert.ToInt32(CompoundElementSplit[0].Split(']')[0].Replace("[", ""));
                    
                    // Parse the spaces outta there, we dont need them
                    string ParsedSMILE = CompoundElementSplit[1].Replace(" ", "");
                    SkippedCompoundsDictionary.Add(NumberCompound, ParsedSMILE);

                }
                
            }

            Console.Clear();
            Log.WriteLine("\n------------------------------------------------------", ConsoleColor.Blue);
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

            using (StreamWriter sw = new StreamWriter(FileData.SkippedSMILES_BiotransformerInput)) 
            {
                foreach (var info in SkippedCompoundsDictionary)
                {
                    string name = ($"{listName}" + ".cmpnd" + info.Key + "." + fileType);
                    string bio = "java -jar \"biotransformer-1.1.5 (1).jar\" -k pred -b " + metabolismType + " -ismi \"" + info.Value + "\" -o" + fileType + " " + name + " -s 1";
                    sw.WriteLine(bio);
                }
                
            }
        }

        public static List<string> BioTransformerCMDS = new List<string>();
        public static List<string> SkippedSMILES = new List<string>();

        public static void Settings()
        {
            bool looping = true;
            Config cfg = new Config();
            while (looping)
            {
                Console.Clear();

                cfg.ReloadConfig();
                Log.WriteLine("Biotransformer-Assistant Settings", ConsoleColor.Red);
                Log.WriteLine("To change the value of a setting, type its associated number. To return back, do \"exit\"\n", ConsoleColor.Yellow);
                Log.WriteLine("[1] Open Associated File", ConsoleColor.Blue);

                if (cfg.OpenFiles)
                    Log.WriteLine("\t[Enabled]", ConsoleColor.Green);
                else
                    Log.WriteLine("\t[Disabled]", ConsoleColor.Red);

                Log.WriteLine("\n[2] Debug Log", ConsoleColor.Blue);
                if (cfg.DebugLog)
                    Log.WriteLine("\t[Enabled]", ConsoleColor.Green);
                else
                    Log.WriteLine("\t[Disabled]", ConsoleColor.Red);

                int selection;
                string strSelection = Console.ReadLine();
                if (int.TryParse(strSelection, out selection))
                {
                    switch (selection)
                    {
                        case 1:
                            if (cfg.OpenFiles)
                                cfg.ModifyingOpenFile(false);
                            else if (!cfg.OpenFiles)
                                cfg.ModifyingOpenFile(true);
                            break;
                        case 2:
                            if (cfg.DebugLog)
                                cfg.ModifyDebugLog(false);
                            else if (!cfg.DebugLog)
                                cfg.ModifyDebugLog(true);
                            Log.Debug("Modifying Debug Log Setting...");
                            break;
                        default:
                            Log.Error("Invalid Selection");
                            Thread.Sleep(2000);
                            break;

                    }
                }
                else if (strSelection.Contains("exit"))
                {
                    Console.Clear();
                    StartMenu();
                    looping = false;
                }
                


            }

        }

        public static void GenerateBiotransformerStringData(string listName, string fileType, string metabolismType)
        {
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

            CreateAndGenerateFiles();
        }
        
        public static void PubChemParsingSMILES()
        {
            Log.Info("Instructions for next section :\n");
            Log.List(1, "Go to “PubChem Identifier Exchange Service” in any web browser\n" +
                            "\tAvailable at: https://pubchem.ncbi.nlm.nih.gov/idexchange/idexchange.cgi\n");
            Log.List(2, "Set “Input ID List” to “Synonyms”");
            Log.List(3, "Select “Choose File“ and select the file in the Output Folder, labeled “PubChemCompounds“");
            Log.List(4, "Set “Operator Type” to “Same CID”");
            Log.List(5, "Set the Output IDs to “SMILES“");
            Log.List(6, "Set “Output Method” to “Two column file showing each input-output correspondence”");
            Log.List(7, "Set “Compression” to “No Compression”");
            Log.List(8, "Press “Submit Job” at the bottom left");
            Log.List(9, "It may take a few moments for PubChem to process the data. \n\tOnce the text has loaded, copy and paste this output into the file called “Compound_SMILES“. Save and close the file");
            Log.Warning("PubChem may not be able to convert every compound into a SMILES format. As such, those compounds will be skipped in future output and will have to be done manually.");

            Log.Info("When you have pasted the data into the PubChemOutput File, Press \'Enter\' to start the parsing process");
            Console.ReadLine();
            Log.Warning("This may take several seconds, please be patient!");

            bool fileSizeCorrect = false;
            while (!fileSizeCorrect)
            {
                if (PostPubChem.ChemFileSize() <= 1)
                {
                    Log.Error("There is no compounds in “Compound_SMILES“\nPress “ENTER“ when there are compounds");
                    Console.ReadLine();
                }
                else
                    fileSizeCorrect = true;
            }

            PostPubChem.LoadChemFile();

            Log.Success("Done! Press “ENTER“ to continue");
            Console.ReadLine();
            PromptBiotransformerInput();
        }

        public static void LoadCompounds()
        {
            Config cfg = new Config();
            cfg.ReloadConfig();

            Log.Info("\n[1] Please paste unparsed compounds, one per line, into the text document “RawCompounds”\n" +
                "[2] Press [CONTROL]+S to save, then close the file\n\n" +
                "When finished, press “Enter” to continue:");

            Log.Debug(cfg.OpenFiles.ToString());

            if (cfg.OpenFiles)
                Process.Start("notepad.exe", Path.Combine(Directory.GetCurrentDirectory(), FileData.RawCompoundInput));

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
            Console.Clear();
            PubChemParsingSMILES();


        }

        public static void PromptBiotransformerInput()
        {
            Console.Clear();
            Log.WriteLine("\n------------------------------------------------------", ConsoleColor.Blue);
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

            GenerateBiotransformerStringData(listName, fileType, metabolismType);
        }

        public static void CreateAndGenerateFiles()
        {
            string finalOutput = Path.Combine(Directory.GetCurrentDirectory(), "BioTransformerInput.txt");
            File.Delete(FileData.BiotransformerInput);

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

        private static void Exit(string exitMessage)
        {
            Log.Error(exitMessage);
            Log.Error("Press \"Enter\" to exit the program");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public static List<string> Compounds = new List<string>();
        public static List<string> ParsedCompounds = new List<string>();

        public static IEnumerable<string> RawCompoundsFromFile;
        public static List<string> ParsedCompoundsFromFile = new List<string>();

    }
}
