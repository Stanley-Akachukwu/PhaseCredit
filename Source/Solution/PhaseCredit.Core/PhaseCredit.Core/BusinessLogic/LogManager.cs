using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhaseCredit.Core.BusinessLogic
{
    public class LogManager : ILogManager
    {

        private readonly ILogger<LogManager> _logger;
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public LogManager(ILogger<LogManager> logger)
        {
            _logger = logger;
        }
       
        public Task  Log<T>(string info,T instance, LogLevel logLevel)
        {
            _logger.LogInformation($"Excuted {instance?.GetType().FullName} with:");
            _logger.LogInformation(info, JsonSerializer.Serialize(instance, _serializerOptions));
            return Task.CompletedTask;
        }

        public Task LogData(string title, string name, string data, LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

    }
}
