using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;

namespace PROSITE.Core.Parsers
{
    public class RepetitionParser: IAtomParser
    {
        public IAtom Parse(string expression, ref int cursor, IList<IAtom> atoms)
        {
            var nums = new List<int>(2);
            if (expression[cursor] == '(')
            {
                string curr = "";
                bool end = false;
                while (cursor < expression.Length -1 && !end)
                {
                    cursor++;
                    char c = expression[cursor];
                    switch (c)
                    {
                        case ')':                            
                        case ',':
                            int n;
                            if (!Int32.TryParse(curr, out n))
                                throw new ParserException(String.Format("Not a number: {0} at pos {1}",  curr, cursor), cursor);
                            nums.Add(n);
                            curr = "";
                            end = c == ')';
                            break;
                        default:
                            if (!Char.IsDigit(c))
                                throw new ParserException(String.Format("Expected numeric token or , or ), found {0} at position {1}", c, cursor), cursor);
                            curr += c;
                            break;
                    }
                }
                
                if (!nums.Any() || nums.Count() > 2)
                    throw new ParserException("Invalid argument count for repetition: {0}", nums.Count);
                if (atoms.LastOrDefault() == null || atoms.Last() is DashAtom)
                    throw new ParserException("No expression to repeat", cursor);
                if (nums.Count == 2 && nums[0] >= nums[1])
                    throw new ParserException(String.Format("Invalid repetition range {0}, {1}", nums[0], nums[1]), cursor);
                var atom = atoms.Last();
                atoms.RemoveAt(atoms.Count - 1);
                return new RepetitionAtom(atom, nums.ToArray());
            }
            return null;
        }
    }
}
