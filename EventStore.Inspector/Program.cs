using CommandLine;
using EventStore.Inspector.Common;
using Serilog;

namespace EventStore.Inspector
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsedAsync(options => {

                var connectionOptions = OptionsTranslator.ConnectionOptionsFrom(options);
                var settings = OptionsTranslator.From(options);

                Log.Logger = Logging.For(options);

                return Search.Create(connectionOptions).For(settings);
            });
        }
    }
}
