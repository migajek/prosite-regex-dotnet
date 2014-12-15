using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;

namespace PROSITE.Core.Parsers
{
    public class AcidInSetParser: IAtomParser
    {
        public IAtom Parse(string expression, ref int cursor, IList<IAtom> atoms)
        {
            char c = expression[cursor];
            if (c == '{' || c == '[')
            {
                var chars = new List<char>();
                while (cursor < expression.Length-1)
                {
                    cursor++;
                    var current = expression[cursor];
                    if ((c == '{' && current == '}') || (c == '[' && current == ']'))
                        break;
                    if (!AminoAcidHelper.AminoAcidLetters.Contains(current))
                        throw new ParserException(String.Format("Unexpected token {0} at position {1}, expected only amino acid letters", current, cursor), cursor);
                    chars.Add(current);
                }
                return new AcidSetAtom(chars, c == '{');
            }

            return null;
        }
    }

}
