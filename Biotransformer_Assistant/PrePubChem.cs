using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Biotransformer_Assistant
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

    }
}
