using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;

namespace gisa.comum
{
    public class AgenteLog
    {
        public ILogger CriaAgente(IConfiguration configuration)
        {
            //Log.Logger = new LoggerConfiguration().CreateLogger();           
            //return new LoggerConfiguration()
            //.WriteTo.File(Path.Combine(pathLog,"log-.txt"), rollingInterval: RollingInterval.Day)
            //.CreateLogger();
            return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }

    }
}
