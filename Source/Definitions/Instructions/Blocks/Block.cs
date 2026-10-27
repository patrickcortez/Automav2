using Automav2.Source.Definitions.Varirable;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Definitions.Instructions.Blocks
{
    internal abstract class Block : Instruction
    {
        public abstract override Task Execute(IEnumerable<Variable> variables);
        public Instruction? Route { get; set; }
    }
}
