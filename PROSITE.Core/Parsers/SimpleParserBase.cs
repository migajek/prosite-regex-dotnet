using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;

namespace PROSITE.Core.Parsers
{
    public abstract class SimpleParserBase<TAtom>: IAtomParser
        where TAtom: class, IAtom, new()
    {
        private readonly char[] _triggers;

        protected SimpleParserBase(params char[] trigger)
        {
            this._triggers = trigger;
        }

        public IAtom Parse(string expression, ref int cursor, IList<IAtom> atoms)
        {
            return _triggers.Contains(expression[cursor]) ? new TAtom() : null;
        }
    }
}
