using System;
using CommandLine;

namespace EventStore.Inspector
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options => {
                Console.WriteLine("SearchProperty: " + string.Join(",", options.SearchProperty));
                Console.WriteLine("SearchText: " + string.Join(",", options.SearchText));
                Console.WriteLine("OutputFormat: " + options.OutputFormat);
                Console.WriteLine("Stream: " + options.Stream);
            });
        }
    }
}
