using Microsoft.Extensions.Logging;
using System;
using static System.Console;
namespace WorkingWithEFCore
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }

        public void Dispose(){}
    }
    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState tstate)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                    return false;
                case LogLevel.Debug:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                default:
                    return true;
            };
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,TState state, Exception exception, Func<TState,Exception,string> formatter)
        {

            if(eventId.Id == 20100)
            {
                // log the level and event identifier
                Write($"Level: {logLevel}, Event ID: {eventId.Id}, Event: {eventId.Name}");
                
                // only output the state or exception if it exists
                
                if (state != null)
                {
                    Write($", State: {state}");
                }
                
                if (exception != null)
                {
                    Write($", Exception: {exception.Message}");
                }
                
                WriteLine();
            }
        
        }
        
    }
}