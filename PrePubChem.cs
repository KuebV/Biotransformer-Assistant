using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Biotransformer
{
    public class PrePubChem
    {
        /// <summary>
        /// Removes any additional text or lettering that comes after the compound that PubChem cannot handle
        /// </summary>
        /// <param name="Compound"></param>
        /// <returns>Parsed Compound</returns>
        public static string ParseCompounds(string Compound)
        {
            if (Compound.Contains("Esi+"))
            {
                int indexOfPlus = Compound.IndexOf("Esi+");
                if (indexOfPlus >= 0)
                    Compound = Compound.Substring(0, indexOfPlus);
            }

            if (Compound.Contains('+'))
            {
                int indexOfPlus = Compound.IndexOf('+');
                if (indexOfPlus >= 0)
                    Compound = Compound.Substring(0, indexOfPlus);
            }

            return Compound;
        }
        public static void SeralizeFiles()
        {
            string pubChemOutput = Path.Combine(Directory.GetCurrentDirectory(), "PubChemOutput.txt");
            string input = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
            if (!File.Exists(pubChemOutput))
            {
                Log.Error("PubChemOutput.txt does not exist, you must setup the program first!");
                Log.Error("Press \'Enter\' to exit the program");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (!File.Exists(input))
            {
                Log.Error("input.txt does not exist, you must setup the program first!");
                Log.Error("Press \'Enter\' to exit the program");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

    }
}
