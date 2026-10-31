using Automav2.Source.Definitions.Varirable;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Definitions
{
    internal abstract class Expression
    {
        public abstract object Eval(IEnumerable<Variable> Scope);

        internal class LiteralExpression(IValue<object> Lvalue) : Expression
        {
            public override IValue<object> Eval(IEnumerable<Variable> Scope) => Lvalue;
        }
        internal class VariableExpression(string Other) : Expression
        {
            public override Variable Eval(IEnumerable<Variable> Scope) => Scope.FirstOrDefault(item => item.Name == Other) ??
                throw new Exception($"Variable {Other} not found!");
        }

         // internal class FunctionExpression(string FuncName,Parameter) -- UpComing Functions 

    }
}
