using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Parsers;

namespace PROSITE.Core.Atoms
{
    public class WildcardAtom: IAtom
    {
        public bool Matches(string data, ref int cp)
        {
            if (AminoAcidHelper.AminoAcidLetters.Contains(data[cp]))
            {
                cp++;
                return true;
            }
            return false;
        }

        public int ExpectedMatchLength
        {
            get { return 1; }
        }

        public override string ToString()
        {
            return "x";
        }
    }
}
