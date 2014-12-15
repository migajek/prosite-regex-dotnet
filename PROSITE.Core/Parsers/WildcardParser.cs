using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;

namespace PROSITE.Core.Parsers
{
    public class WildcardParser: SimpleParserBase<WildcardAtom>
    {
        public WildcardParser() : base('x')
        {
        }
    }
}
