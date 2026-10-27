using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Automav2.Source.Definitions.Varirable
{
    // Variable Decleration

    internal class Variable(string Name, IValue<object> Value)
    {
        public string Name { get; } = Name;
        public object Value { get; set; } = Value;  
    }

    internal abstract class IValue<T>
    {
        public abstract T? Eval();
    }

    internal class AutomaInteger(int Value) : IValue<int>
    {
        public override int Eval() => Value;
    }
    internal class AutomaString( string Value) : IValue<string>
    {
        public override string Eval() => Value;
    }

    internal class AutomaBoolean(bool Value) : IValue<bool>
    {
        public override bool Eval() => Value;
    }

    internal class AutomaNull : IValue<object>
    {
        public override object? Eval() => null;
    }
}
