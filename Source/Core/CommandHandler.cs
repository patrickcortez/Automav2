using Automav2.Source.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Core
{
    internal class CommandHandler(string cmd,string[]? args)
    {

       private Dictionary<string, Func<string[], Task<int>>> Commands = new()
        {
            {
                "run",
                async (string[] arg) =>
                {

                     bool isdebug = (args.Length>1)?args[1].ToLower() == "-d":false;

                     string fpath = args[0];
                     Lexer lex = new(fpath,isdebug);
                     return await lex.Start();
                }


            },

           {
               "version",
               async (string[] args) =>
               {
                   await AutomaIO.Write("AutomaV2 1.0.0");
                   return 0;
               }
           }
        };

        public async Task<int> Run()
        {
            try
            {

                return await Commands[cmd](args);
            }
            catch(Exception ex)
            {
                await AutomaIO.Write(ex);
                return 1;
            }
        }

    }
}
