using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Share.Services
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message) => _logger.LogInformation($"{typeof(T).Name}: {message}");
        public void LogWarning(string message) => _logger.LogWarning($"{typeof(T).Name}: {message}");
        public void LogError(string message) => _logger.LogError($"{typeof(T).Name}: {message}");
    }
}
