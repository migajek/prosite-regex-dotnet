using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;

namespace PROSITE.Core.Parsers
{
    public class AminoAcidParser: IAtomParser
    {
        public IList<char> AllowedLetters { get; private set; }

        public AminoAcidParser()
        {
            AllowedLetters = AminoAcidHelper.AminoAcidLetters;
        }

        public IAtom Parse(string expression, ref int cursor, IList<IAtom> atoms)
        {
            return AllowedLetters.Contains(expression[cursor]) ? new AminoAcidLetter(expression[cursor]) : null;
        }
    }
}
