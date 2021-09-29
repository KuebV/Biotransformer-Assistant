using System;
using System.Collections.Generic;
using System.IO;

namespace Biotransformer_Assistant
{
    class Program
    {
        /// <summary>
        /// Dear whoever decides to read this, and wonder to themselves why this 80% of this program is located in the Main Method
        /// I dont know, I'm quite lazy, and this works perfectly fine
        /// Sure it might not look the best, but it does its job
        /// </summary>
        /// <param name="args"></param>
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
