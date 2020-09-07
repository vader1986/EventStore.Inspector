using CommandLine;
using EventStore.Inspector.Common.Search;
using EventStore.Inspector.Common.Analysis;
using Serilog;

namespace EventStore.Inspector
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<SearchOptions, AnalyseOptions>(args)
                .WithParsed<SearchOptions>(options => {
                    Log.Logger = Logging.For(options);
                    Search
                        .Create(OptionsTranslator.ConnectionOptionsFrom(options))
                        .For(OptionsTranslator.From(options)).Wait();
                })
                .WithParsed<AnalyseOptions>(options => {
                    Log.Logger = Logging.For(options);
                    Analysis
                        .Create(OptionsTranslator.ConnectionOptionsFrom(options))
                        .For(OptionsTranslator.From(options)).Wait();
                });
        }
    }
}
