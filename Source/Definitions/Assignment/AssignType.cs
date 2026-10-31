using Automav2.Source.Definitions.Instructions;
using Automav2.Source.Definitions.Varirable;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Automav2.Source.Definitions.Assignment
{


    internal class AssignmentInstruction(string Target,Expression Value) : Instruction
    {
        public override Task Execute(IEnumerable<Variable> Scope)
        {
            Variable? found = (Scope.Count() > 0) ? Scope.FirstOrDefault(item => item.Name == Target):null;

            if(found is not null)
            {
                found.Value = Value switch
                {
                    LiteralExpression literal => literal.Eval(Scope),
                    VariableExpression variable => variable.Eval(Scope).Value,
                    _ => throw new Exception("Invalid ASsignment Type!")
                };
            }
            else
            {
                IValue<object> Lvalue = Value switch
                {
                    LiteralAssignment literal => literal.Eval(Scope),
                   VariableAssignment variable => variable.Eval(Scope).Value,
                    _ => throw new Exception("Invalid ASsignment Type!")
                };
                found = new(Target,Lvalue);
                
                var tmp = Scope.ToList();
                tmp.Add(found);
                Scope = new List<Variable>(tmp);
                
            }

            return Task.CompletedTask;
        }
    }
}
