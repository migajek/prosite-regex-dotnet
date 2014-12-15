using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROSITE.Core.Atoms;

namespace PROSITE.Core
{
    public class CompiledExpression : List<IAtom>
    {
        public CompiledExpression(string expression)
        {
            this.AddRange((new ExpressionCompiler()).ParseExpression(expression));
        }

        public CompiledExpression()
        {
            
        }
    }
}
