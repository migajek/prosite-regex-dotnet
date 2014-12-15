using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;
using PROSITE.Core.Parsers;

namespace PROSITE.Core
{
    public class ExpressionTester
    {        
        public IEnumerable<Match> GetMatches(CompiledExpression expressions, string data)
        {            
            for (int i = 0; i < data.Length; i++)
            {
                int cp = i;
                
                var isMatch = IsMatch(expressions, data, ref cp);
                if (isMatch)
                {
                    yield return new Match(data.Substring(i, cp - i), i);
                    //i = cp - 1;
                }

            }
        }

        private bool IsMatch(CompiledExpression expressions, string data, ref int cp)
        {
            bool isMatch = true;
            foreach (var item in expressions.Select((e, idx) => new {e, idx}))
            {
                var expression = item.e;
                if (expression is IGreedyAtom)
                {
                    var matchRange = (expression as IGreedyAtom).Matches(data, cp);
                    if (!matchRange.HasValue)
                    {
                        isMatch = false;
                        break;
                    }
                    bool found = false;
                    for (int pos = Math.Min(data.Length, matchRange.Value.End);
                        pos >= matchRange.Value.Start;                        
                        pos--)
                    {
                        var subExpr = new CompiledExpression();
                        subExpr.AddRange(expressions.Skip(item.idx + 1));
                        string subString = data.Substring(pos);
                        Debug.WriteLine("recursive test, substring: '{0}' against expr '{1}'", subString,
                            String.Join("", subExpr.Select(x => x.ToString())));
                        int tmpCp = 0;
                        if (IsMatch(subExpr, subString, ref tmpCp))
                        {
                            cp = pos;
                            Debug.WriteLine("Subexpr matches, moving cursor to {0}, remaining '{1}'", cp, data.Substring(cp));
                            found = true;
                            break;
                        }                        
                    }
                    if (!found)
                    {
                        isMatch = false;
                        break;
                    }
                }
                else
                {
                    var matchLen = expression.ExpectedMatchLength;
                    if (cp > data.Length - matchLen || !expression.Matches(data, ref cp))
                    {
                        isMatch = false;
                        break;
                    }
                }
            }
            return isMatch;
        }

        public IEnumerable<Match> GetMatches(string expressions, string data)
        {
            var parser = new ExpressionCompiler();
            return GetMatches(parser.ParseExpression(expressions), data);
        }
    }
}
