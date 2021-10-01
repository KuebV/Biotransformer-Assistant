using System;
using System.Collections.Generic;
using System.IO;

namespace Biotransformer_Assistant
{
    class Program
    {
        public static string ProgramVersion = "1.2.2";
        static void Main(string[] args)
        {
            Log.Debug("Starting Process...");
            Menu.StartMenu();
        }

        public static void End()
        {
            Console.Clear();
            Log.WriteLine("---------------------------------------------------------------------------------------------------", ConsoleColor.Blue);
            Log.Success("Done!");
            Log.Info("Data processing successful! You have three new data sets available:");
            Log.WriteLine("\n__________________________________________________________________", ConsoleColor.Blue);

            Log.List(1, "BioTransformer Input");
            Log.WriteLine("\n\t- The file “BioTransformer_Input” now contains the proper BioTransformer input, which can be directly copied/pasted into the program.");

            Log.List(2, "SMILES Spreadsheet Key");
            Log.WriteLine("\n\t- The file “SMILEs_SpreadsheetKey” now contains the SMILEs format for each compound, which can be directly copied/pasted into a spreadsheet.");

            Log.List(3, "Skipped Compounds");
            Log.WriteLine("\n\t- The file “Skipped_Compounds” now contains the list of compounds, with their associated indices from “STEP1”, that PubChem could not find corresponding SMILEs formats for.");

            Console.ReadLine();
        }
    }
}
