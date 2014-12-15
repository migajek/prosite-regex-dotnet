using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core;

namespace PROSITE.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length  != 2)
            {
                Console.WriteLine("Usage: prosite.tester <expression> <data>");
                return;
            }
            
            string expr = args.First();
            string data = args.Last();
            var tester = new ExpressionTester();
            try
            {
                var results = tester.GetMatches(expr, data);
                foreach (var result in results)
                {
                    Console.WriteLine("[{0}]: {1}", result.Position, result.MatchedText);
                }                
            }
            catch (ParserException pe)
            {
                Console.WriteLine("Expression syntax error: " + pe.Message);
                Console.WriteLine(expr);
                var s = String.Concat(Enumerable.Range(0, expr.Length).Select(i => i == pe.Position ? '^' : '-'));
                Console.WriteLine(s);
            }

            
        }
    }
}
