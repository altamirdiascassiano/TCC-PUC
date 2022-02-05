using gisa.comum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace gisa.mic.backend
{
    public class Startup
    {
        AgenteSQS _agenteSQS;
        AgenteFireBaseStorage _agenteFireBaseStorage;
        public Startup(IConfiguration configuration)
        {           
            Log.Logger = new AgenteLog().CriaAgente(configuration);
            Log.Debug("Startup.cs -> Startup()");

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddLogging().AddLogging();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "gisa.mic.backend", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors();
            _agenteSQS = new AgenteSQS("", "", @"");
            _agenteFireBaseStorage = new AgenteFireBaseStorage();
            services.AddSingleton(_agenteSQS);
            services.AddSingleton(_agenteFireBaseStorage);
            Log.Debug("Startup.cs -> ConfigurationService()");                   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "gisa.mic.backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            Log.Debug("Startup.cs -> Configure()");
            Log.Information("gisa.mic.backend iniciado");
        }
    }
}
