using Automav2.Source.Core.Builders;
using Automav2.Source.Core.IO;
using Automav2.Source.Definitions;
using Automav2.Source.Definitions.Instructions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Core.Parser
{
    internal class Parser
    {
        private Tokens[] Toks { get; }
        private bool IsDebug { get; }

        public Parser(Tokens[] toks, bool isDebug=false)
        {
            Toks = toks;
            IsDebug = isDebug;
        }

        private async Task<Instruction?> Parse()
        {
            try
            {
                NodeBuilder Bob = new(IsDebug);
                StringBuilder CurrentKeyWord= new StringBuilder();


                for(int i = 0;i < Toks.Length; i++)
                {
                    Tokens CurrentToken = Toks[i];
                    TokenType CurrentType = CurrentToken.Type;


                    if(CurrentType is TokenType.Token_Keyword)
                    {
                        
                    }

                }

                return Bob.GetNode();
            }
            catch (Exception ex) {
                await AutomaIO.Write(ex);
                return null;
            
            
            }
            


        }

        public async Task<int> Start()
        {
            try
            {



                return 0;
            }catch(Exception ex)
            {
                await AutomaIO.Write(ex);
                return 1;
            }
        }

    }
}
