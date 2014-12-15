using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROSITE.Core.Atoms
{

    public class AcidSetAtom : IAtom
    {
        public List<char> Chars { get; private set; }
        public bool Negated { get; private set; }

        public AcidSetAtom(List<char> chars, bool negated)
        {
            Chars = chars;
            Negated = negated;
        }

        public bool Matches(string data, ref int cp)
        {            
            var contains = Chars.Contains(data[cp]);
            if (contains ^ Negated)
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
            return String.Format("{0}{1}{2}", Negated ? '{' : '[',
                new string(this.Chars.ToArray()), Negated ? '}' : ']');
        }
    }
}
