using System;

namespace CommandParser
{
    class Program
    {
        static void Help()
        {
            Console.Out.WriteLine("CommandParser.exe - command line parser.");
            Console.Out.WriteLine("Usage:");
            Console.Out.WriteLine("CommandParser.exe [/?] [/help] [-help] [-k key value] [-ping] [-print <value>]");
            Console.Out.WriteLine("/?, /help, -help - show help");
            Console.Out.WriteLine("-k [key value] - CommandParser.exe -k key1 value1 key2 value 2 - show key-value table in format");
            Console.Out.WriteLine("key1 - value1");
            Console.Out.WriteLine("key2 - value2");
            Console.Out.WriteLine("-ping - beep and show \"Pinging …\" in console");
            Console.Out.Write("-print <value> - print message <value>");
        }
        static void ParseHelp(string[] args)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                switch (args[i])
                {
                    case "/?":
                    case "/help":
                    case "-help":
                        Help();
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static bool CheckCommand(string str)
        {
            return str[0] == '-' || str[0] == '/';
        }
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Help();
                return;
            }
            ParseHelp(args);
            int i = 0;
            do
            {
                switch (args[i])
                {
                    case "-ping":
                        Console.Beep();
                        Console.Out.WriteLine("Pinging...");
                        break;
                    case "-print":
                        if ((++i < args.Length) && !CheckCommand(args[i]))
                        {
                            Console.Out.WriteLine(args[i]);
                        }
                        else
                        {
                            Console.Out.WriteLine("Message is not given");
                            Console.Out.WriteLine("use CommandParser.exe /? to see set of allowed commands");
                        }
                        break;
                    case "-k":
                        do
                        {
                            ++i;
                            if ((i + 1) < args.Length && !CheckCommand(args[i]) && !CheckCommand(args[i+1]))
                            {
                                Console.Out.WriteLine(args[i++]+" - "+args[i]);
                            }
                            else
                            {
                                Console.Out.WriteLine("Key-value pair is not given");
                                Console.Out.WriteLine("use CommandParser.exe /? to see set of allowed commands");
                                break;
                            }
                        }
                        while ((i+1 < args.Length) && (!CheckCommand(args[i+1])) );
                        break;
                    default:
                        Console.Out.WriteLine("Command "+ args[i] + " is not supported, use CommandParser.exe /? to see set of allowed commands");
                        return;
                }
                ++i;
            }
            while (i < args.Length);
        }
    }
}
