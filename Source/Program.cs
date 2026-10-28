using Automav2.Source.Core;
using static Automav2.Source.Core.IO.AutomaIO;
namespace Automav2.Source;

public static class Program{

    public static async Task<int> Main(string[] args)
    {

        Console.TreatControlCAsInput = true;

        try
        {

            if(args.Length == 0)
            {
                CommandHandler command1 = new("version", []);
                await command1.Run();
                return 0;
            }

            

            string cmd = args[0].ToLower();
            string[] arg = args.Skip(1).ToArray();

            CommandHandler command = new(cmd, (args.Length > 0) ? arg : []);

            return await command.Run();

        }catch(Exception ex)
        {

            await Write(ex);
            return 1;
        }

    }

}