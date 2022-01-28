using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;

namespace gisa.comum
{
    public class AgenteLog
    {
        public ILogger CriaAgente(IConfiguration configuration)
        {
            return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }

    }
}
