using System.Collections;
using System.Collections.Generic;
using PROSITE.Core.Atoms;

namespace PROSITE.Core.Parsers
{
    public interface IAtomParser
    {
        IAtom Parse(string expression, ref int cursor, IList<IAtom> atoms);
    }
}
