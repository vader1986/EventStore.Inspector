using Serilog;

namespace EventStore.Inspector
{
    public static class Logging
    {
        public static ILogger For(ConnectionOptions options)
        {
            if (options.Verbose)
            {
                return new LoggerConfiguration().MinimumLevel.Debug().WriteTo.ColoredConsole().CreateLogger();
            }

            return new LoggerConfiguration().WriteTo.ColoredConsole().CreateLogger();
        }
    }
}
