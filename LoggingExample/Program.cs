using Microsoft.Extensions.Logging;

namespace LoggingExample
{
    internal class Program
    {
        static void Main()
        {
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Information)
                .AddConsole());
            var _logger = loggerFactory.CreateLogger<Program>();
            Log(_logger);
            LogSourceGenerated(_logger);
            Console.ReadKey();
        }

        private static void Log(ILogger<Program> _logger)
        {
            //if (_logger.IsEnabled(LogLevel.Information)) //should always be used
            {
                var foo = "Foo";
                var message = $"There is a {foo}";
                _logger.LogInformation(message);
                //_logger.LogInformation("There is a {param1}", foo); // to make compile-message go away
            }
        }

        private static void LogSourceGenerated(ILogger<Program> _logger)
        {
            //suggested way (from MS): Always use source-generated logging so IsEnabled() and Define() is always used
            _logger.LogInformationExt("Foo");
            //LoggingExtensions.LogInformationStatic(_logger, "Foo");
        }
    }

    public static partial class LoggingExtensions
    {
        [LoggerMessage("There is a {foo}", Level = LogLevel.Information)]
        //[LoggerMessage("There is a {foo", Level = LogLevel.Information)] // compile error
        public static partial void LogInformationExt(this ILogger logger, string foo);


        //[LoggerMessage("There is a {foo}", Level = LogLevel.Information)]
        //public static partial void LogInformationStatic(ILogger logger, string foo);
    }
}
