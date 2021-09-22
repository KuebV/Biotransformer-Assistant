using System;
using System.Collections.Generic;
using System.Text;

namespace Biotransformer
{
    public class PrePubChem
    {
        /// <summary>
        /// Must be given individual compounds
        /// </summary>
        /// <param name="Compound"></param>
        /// <returns></returns>
        public static string ParseCompounds(string Compound)
        {
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
