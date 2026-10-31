using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Automav2.Source.Definitions.Assignment;
using Automav2.Source.Definitions.Varirable;
using static Automav2.Source.Core.IO.AutomaIO;

namespace Automav2.Source.Definitions.Instructions
{
    internal class WriteInstruction(string Content) : Instruction
    {
        private string ExpandString(string Input,IEnumerable <Variable> Scope)
        {
            Regex clean = new("\\$([a-zA-Z_][a-zA-Z0-9_])*");

            return clean.Replace(Input, match =>
            {
                string VarName = match.Groups[1].Value;

                Variable? Target = Scope.Where(item => item.Name == VarName).FirstOrDefault();

                if(Target is not null)
                {
#pragma warning disable CS8603
                    return Target.Value.ToString();
                }

                return VarName;

            });

        }

        public override async Task Execute(IEnumerable<Variable> Scope)
        {

            string Expanded = ExpandString(Content,Scope);

            await Write(Expanded);
        }
    }

    internal class ReadAssignment(string Content) : IAssignType
    {

        private string ExpandString(string Input, IEnumerable<Variable> Scope)
        {
            Regex clean = new("\\$([a-zA-Z_][a-zA-Z0-9_])*");

            return clean.Replace(Input, match =>
            {
                string VarName = match.Groups[1].Value;

                Variable? Target = Scope.Where(item => item.Name == VarName).FirstOrDefault();

                if (Target is not null)
                {
#pragma warning disable CS8603
                    return Target.Value.ToString();
                }

                return VarName;

            });

        }

        public async Task<string>? Execute(IEnumerable<Variable> Scope)
        {

            string filtered = ExpandString(Content,Scope);
            await Write(Content);
            return await Read();
        }
    }
}
