using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Core
{
    internal class CommandHandler(string cmd,string[]? args)
    {

       private Dictionary<string, Func<string[], int>> Commands = new()
        {
            {
                "run",
                (string[] arg) => 
                {
                    return 0; // insert interpreter here
                }
            }
        };

    }
}
