﻿using System.Collections.Generic;
using CommandLine;
using EventStore.Inspector.Common;
using EventStore.Inspector.Common.Search;
using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector
{
    public class ConnectionOptions
    {
        [Option('c', "connection", Required = false, HelpText = "Connection string for the EventStore.")]
        public string ConnectionString { get; set; } = "tcp://admin:changeit@localhost:1113";

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; } = false;

        [Option('s', "stream", Required = false, HelpText = "Event stream to inspect (default: $ce-all).")]
        public string Stream { get; set; } = "$ce-all";

        [Option("batch", Required = false, HelpText = "Number of events to process within one batch (default: 100)")]
        public int BatchSize { get; set; } = 100;

        [Option("mode", Required = false, HelpText = "What to do after processing one batch: AwaitUserInput/Sleep/Continue (default: Sleep)")]
        public BatchMode BatchMode { get; set; } = BatchMode.Sleep;

        [Option("sleep", Required = false, HelpText = "Number of milliseconds to sleep after each batch (requires Sleep mode, default 500)")]
        public int SleepIntervalMilliSeconds { get; set; } = 500;

        [Option("forward", Required = false, HelpText = "Whether to start processing events from beginning or end of the stream (default: false)")]
        public bool ReadForward { get; set; } = false;
    }

    [Verb("search", HelpText = "Search for events in a stream")]
    public class SearchOptions : ConnectionOptions
    {
        [Option('t', "text", Required = false, HelpText = "Text to search the event stream for.")]
        public IEnumerable<string> SearchText { get; set; } = new string[0];

        [Option('j', "json", Required = false, HelpText = "Specify a JSON property to search for: <key>:<value>")]
        public IEnumerable<string> SearchProperty { get; set; } = new string[0];

        [Option('r', "regex", Required = false, HelpText = "Specify a regular expression to search for.")]
        public IEnumerable<string> SearchRegex { get; set; } = new string[0];

        [Option('o', "output", Required = false, HelpText = "Output formats: Text/Json (default: text).")]
        public OutputFormat OutputFormat { get; set; } = OutputFormat.Text;

        [Option('a', "aggregate", Required = false, HelpText = "Specify how to aggregate multiple search functions: or/and (default: or).")]
        public AggregationMethod Aggregation { get; set; } = AggregationMethod.Or;
    }

    [Verb("analyse", HelpText = "Analyse stats for a particular stream")]
    public class AnalyseOptions : ConnectionOptions
    {
        [Option('t', "type", Required = false, HelpText = "Specify a filter for event type (default: empty, any type is allowed).")]
        public IEnumerable<string> EventTypes { get; set; } = new string[0];
    }
}