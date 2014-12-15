using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROSITE.Core.Atoms
{
    public class DashAtom: IAtom
    {
        public bool Matches(string data, ref int cp)
        {
            return true;
        }

        public int ExpectedMatchLength
        {
            get { return 0; }
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
