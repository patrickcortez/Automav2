using Automav2.Source.Definitions.Varirable;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Definitions.Instructions
{
    internal abstract class Instruction
    {
        public abstract Task Execute(IEnumerable<Variable> Scope);
        public Instruction? Next { get; set; }
    }
}
