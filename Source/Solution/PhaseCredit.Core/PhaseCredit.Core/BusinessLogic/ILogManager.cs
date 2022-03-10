using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.BusinessLogic
{
    public interface ILogManager
    {
        Task Log<T>(string title,T instance, LogLevel logLevel);
        Task LogData(string title, string name, string data, LogLevel logLevel);
    }
}
