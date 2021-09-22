using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Biotransformer
{
    public class PostPubChem
    {
        public static IEnumerable<string> PubChemOutputFile;

        public static List<string> PubChemOutputFinalized;
        public static Dictionary<string, string> PubChemSMILES = new Dictionary<string, string>();

        public static string ReplaceWhitespaces(string comp)
        {
            // F**k speed, we dont need it
            string pattern = "\\s+";
            string replacement = " ";
            Regex rx = new Regex(pattern);

            string result = rx.Replace(comp, replacement);
            return result;
        }

        public static void LoadChemFile()
        {
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), "PubChemOutput.txt");
            PubChemOutputFile = File.ReadAllLines(localPath);

            foreach (string chem in PubChemOutputFile)
            {
                if (!chem.Contains('*'))
                {
                    string fixedChem = ReplaceWhitespaces(chem);
                    string[] splitChem = fixedChem.Split(' ');

                    if (!PubChemSMILES.ContainsKey(splitChem[0]))
                        PubChemSMILES.Add(splitChem[0], splitChem[1]);
                }
                else
                    PubChemSMILES.Add(chem, null);
                
            }

        }
    }
}
