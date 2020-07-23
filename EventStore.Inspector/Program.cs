using CommandLine;
using EventStore.Inspector.Common;

namespace EventStore.Inspector
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsedAsync(options => {
                return Search
                    .Create(OptionsTranslator.FromEnv())
                    .For(OptionsTranslator.From(options));
            });
        }
    }
}
