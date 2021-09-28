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
            Menu.StartMenu();

            Log.WriteLine("---------------------------------------------------------------------------------------------------", ConsoleColor.Blue);
            Log.Warning("All data stored in the files “BioTransformer_Input” and “SMILES_SpreadsheetKey” will be overwritten.");
            Log.Info("Press “Enter” to continue");
            Console.ReadLine();

            Console.Clear();

            Log.Success("Done!");

            Log.Info("\n\nProcess Completed. Press \'Enter\' to create a Biotransformer Input & Key File (This will overwrite any existing file)");
            Console.ReadLine();
        }

    }
}
