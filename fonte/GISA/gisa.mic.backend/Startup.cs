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
        AgenteFireBaseStorage _agenteFireBaseStorage;
        AgenteSQS _agenteSQS;        

        public Startup(IConfiguration configuration)
        {           
            Log.Logger = new AgenteLog().CriaAgente(configuration);

            #region FireBaseStorage
            Log.Debug("Buscando configurações do Firebase Storage no appsettings");
            var fbType = configuration.GetValue<string>("FirebaseStorageJsonAuth:type");
            var fbProjectId = configuration.GetValue<string>("FirebaseStorageJsonAuth:project_id");
            var fbPrivatekeyId = configuration.GetValue<string>("FirebaseStorageJsonAuth:private_key_id");
            var fbPrivateKey = configuration.GetValue<string>("FirebaseStorageJsonAuth:private_key");
            var fbClientEmail = configuration.GetValue<string>("FirebaseStorageJsonAuth:client_email");
            var fbClientId = configuration.GetValue<string>("FirebaseStorageJsonAuth:client_id");
            var fbAithUri = configuration.GetValue<string>("FirebaseStorageJsonAuth:auth_uri");
            var fbTokenUri = configuration.GetValue<string>("FirebaseStorageJsonAuth:token_uri");
            var fbAuthProviderX509CertUrl = configuration.GetValue<string>("FirebaseStorageJsonAuth:auth_provider_x509_cert_url");
            var fbClientX509CertUrl = configuration.GetValue<string>("FirebaseStorageJsonAuth:client_x509_cert_url");

            var firebaseStorageJsonAuth = @" 
                    {
                      ""type"": ""{0}"",
                      ""project_id"": ""{1}"",
                      ""private_key_id"": ""{2}"",
                      ""private_key"": ""{3}"",
                      ""client_email"": ""{4}"",
                      ""client_id"": ""{5}"",
                      ""auth_uri"": ""{6}"",
                      ""token_uri"": ""{7}"",
                      ""auth_provider_x509_cert_url"": ""{8}"",
                      ""client_x509_cert_url"": ""{9}""
                    }
            ".Replace("{0}",    fbType                   )
            .Replace("{1}",     fbProjectId              )
            .Replace("{2}",     fbPrivatekeyId           )
            .Replace("{3}",     fbPrivateKey             )
            .Replace("{4}",     fbClientEmail            )
            .Replace("{5}",     fbClientId               )
            .Replace("{6}",     fbAithUri                )
            .Replace("{7}",     fbTokenUri               )
            .Replace("{8}",     fbAuthProviderX509CertUrl)
            .Replace("{8}",     fbClientX509CertUrl      );

            Log.Debug("Criando agente do Firebase Storage");
            _agenteFireBaseStorage = new AgenteFireBaseStorage(fbProjectId,firebaseStorageJsonAuth);

            #endregion

            #region SQS
            Log.Debug("Buscando configurações do Amazon SQS no appsettings");
            var sqsAccessKeyId = configuration.GetValue<string>("SQSJsonAuth:access_key_id");
            var sqsAccessSecretKeyValue = configuration.GetValue<string>("SQSJsonAuth:access_secret_key_value");
            var sqsUrlQueue = configuration.GetValue<string>("SQSJsonAuth:url_queue");
            Log.Debug("Criando agente do Amazon SQS Storage");
            _agenteSQS = new AgenteSQS(sqsAccessKeyId, sqsAccessSecretKeyValue, sqsUrlQueue);
            #endregion
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Debug("Configurando Serviços da API");
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

            services.AddSingleton(_agenteSQS);
            services.AddSingleton(_agenteFireBaseStorage);
            Log.Debug("Finalizado configurações do Serviços da API");
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
            Log.Information("API gisa.mic.backend Iniciada com sucesso");
        }
    }
}
