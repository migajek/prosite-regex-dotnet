using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROSITE.Core.Atoms
{
    class AminoAcidLetter: IAtom
    {
        public AminoAcidLetter(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; private set; }
        public bool Matches(string data, ref int cp)
        {            
            if (data[cp] != Letter) 
                return false;
            cp++;
            return true;
        }

        public int ExpectedMatchLength
        {
            get { return 1; }            
        }

        public override string ToString()
        {
            return Letter.ToString();
        }
    }
}
