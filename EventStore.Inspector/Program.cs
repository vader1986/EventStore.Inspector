using CommandLine;
using EventStore.Inspector.Common.Search;
using EventStore.Inspector.Common.Analysis;
using Serilog;

namespace EventStore.Inspector
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<SearchOptions, AnalyseOptions>(args)
                .WithParsed<SearchOptions>(options => {
                    Environment.BindOptions(options);
                    Log.Logger = Logging.For(options);
                    SearchBuilder
                        .From(OptionsTranslator.ConnectionOptionsFrom(options))
                        .Build()
                        .For(OptionsTranslator.From(options)).Wait();
                })
                .WithParsed<AnalyseOptions>(options => {
                    Environment.BindOptions(options);
                    Log.Logger = Logging.For(options);
                    Analysis
                        .Create(OptionsTranslator.ConnectionOptionsFrom(options))
                        .For(OptionsTranslator.From(options)).Wait();
                });
        }
    }
}
