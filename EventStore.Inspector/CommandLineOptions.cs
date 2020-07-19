using System.Collections.Generic;
using CommandLine;
using EventStore.Inspector.Common;

namespace EventStore.Inspector
{
    public class CommandLineOptions
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; } = false;

        [Option('s', "stream", Required = false, HelpText = "Event stream to inspect (default: $ce-all).")]
        public string Stream { get; set; } = "$ce-all";

        [Option('t', "text", Required = false, HelpText = "Text to search the event stream for.")]
        public IEnumerable<string> SearchText { get; set; }

        [Option('j', "json", Required = false, HelpText = "Specify a JSON property to search for: <key>:<value>")]
        public IEnumerable<string> SearchProperty { get; set; }

        [Option('r', "regex", Required = false, HelpText = "Specify a regular expression to search for.")]
        public IEnumerable<string> SearchRegex { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output formats: text/json (default: text).")]
        public OutputFormat OutputFormat { get; set; }

        [Option('a', "aggregate", Required = false, HelpText = "Specify how to aggregate multiple search functions: or/and (default: or).")]
        public AggregationMethod Aggregation { get; set; }
    }
}
