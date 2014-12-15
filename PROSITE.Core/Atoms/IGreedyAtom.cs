using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROSITE.Core.Atoms
{
    public struct MatchRange
    {
        public int Start { get; set; }
        public int End { get; set; }
    }

    public interface IGreedyAtom
    {
        MatchRange? Matches(string data, int cp);
    }
}
