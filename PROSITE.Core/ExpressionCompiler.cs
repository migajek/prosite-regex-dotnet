using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PROSITE.Core.Parsers;

namespace PROSITE.Core
{
    public class ExpressionCompiler
    {
        private List<IAtomParser> parsers = new List<IAtomParser>();

        public ExpressionCompiler()
        {
            Autodiscover(GetType().Assembly);
        }

        private void Autodiscover(params Assembly[] assemblies)
        {
            var p = assemblies.SelectMany(a => a.GetTypes().Where(t => t.IsPublic && t.IsClass && !t.IsAbstract))
                .Where(t => typeof (IAtomParser).IsAssignableFrom(t)).ToArray()
                .Select(t => Activator.CreateInstance(t) as IAtomParser);
            parsers.AddRange(p);
        }

        public CompiledExpression ParseExpression(string expression)
        {
            var atoms = new CompiledExpression();
            try
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    char chr = expression[i];
                    var atom = parsers.Select(p => p.Parse(expression, ref i, atoms)).FirstOrDefault(p => p != null);
                    if (atom != null)
                        atoms.Add(atom);
                    else
                        throw new ParserException(String.Format("Invalid token {0} at position {1}", chr, i), i);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ParserException("Unexpected end of string", expression.Length - 1);
            }
            return atoms;
        }
    }

    public class ParserException : Exception
    {
        public int Position { get; private set; }
        public ParserException(string message, int position) : base(message)
        {
            Position = position;
        }
    }
}
