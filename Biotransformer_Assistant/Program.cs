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

            Console.Clear();
            Log.WriteLine("---------------------------------------------------------------------------------------------------", ConsoleColor.Blue);
            Log.Success("Done!");
            Log.Info("Data processing successful! You have three new data sets available:");
            Log.WriteLine("\n__________________________________________________________________", ConsoleColor.Blue);
            Log.List(1, "BioTransformer Input");
            Log.WriteLine("- The file “BioTransformer_Input” now contains the proper BioTransformer input, which can be directly copied/pasted into the program.");
            Log.List(2, "SMILES Spreadsheet Key");
            Log.WriteLine("- The file “SMILEs_SpreadsheetKey” now contains the SMILEs format for each compound, which can be directly copied/pasted into a spreadsheet.");

            Console.ReadLine();
        }
    }
}
