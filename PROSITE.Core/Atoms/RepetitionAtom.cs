using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROSITE.Core.Atoms
{
    class RepetitionAtom : IAtom, IGreedyAtom
    {
        public RepetitionAtom(IAtom expression, int[] arguments)
        {
            Arguments = arguments;
            Expression = expression;
        }

        public int[] Arguments { get; private set; }
        public IAtom Expression { get; private set; }

        public bool Matches(string data, ref int cp)
        {

            var sequenceLen = 0;
            int copy = cp;
            while (copy < data.Length && sequenceLen < Arguments.Last() && Expression.Matches(data, ref copy))
                sequenceLen ++;

            if ((Arguments.Length == 1 && sequenceLen == Arguments[0]) 
                ||
                (Arguments.Length == 2 && sequenceLen >= Arguments[0] && sequenceLen <= Arguments[1]))
            {
                cp = copy;
                return true;
            }
            return false;
        }

        public int ExpectedMatchLength
        {
            get { return Arguments.First(); }
        }

        public MatchRange? Matches(string data, int cp)
        {
            int orgCp = cp;
            var sequenceLen = 0;            
            while (cp < data.Length && sequenceLen < Arguments.Last() && Expression.Matches(data, ref cp))
                sequenceLen++;

            if ((Arguments.Length == 1 && sequenceLen == Arguments[0])
                ||
                (Arguments.Length == 2 && sequenceLen >= Arguments[0] && sequenceLen <= Arguments[1]))
            {
                return new MatchRange() {Start = orgCp + Arguments.First(), End = orgCp + sequenceLen};
            }
            return null;
        }

        public override string ToString()
        {
            return String.Format("{1}({0})",String.Join(",", Arguments.Select(a => a.ToString())), Expression);
        }
    }
}
