using static Automav2.Source.Core.IO.AutomaIO;
namespace Automav2.Source;

public static class Program{

    public static async Task<int> Main(string[] args)
    {

        try
        {

            if(args.Length == 0)
            {
                // Run Shell
            }

            string cmd = args[0].ToLower();
            string[] arg = args.Skip(1).ToArray();

            return 0;

        }catch(Exception ex)
        {

            await Write(ex);
            return 1;
        }

    }

}