using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Simem.appCom;
using Simem.AppCom.Base.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Simem.AppCom.Datos.Servicios
{
    /// <summary>
    /// Clase inicializadora
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Inyección configuración
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// configuración servicio
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.WithOrigins("http://localhost:4200") 
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddControllers();
            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Simem",
                    Version = "v1",
                    Description = "Open Api Simem",
                    Contact = new OpenApiContact
                    {
                        Name = "Administrador Simem",
                        Email = "admin@simem.com",
                    }
                });
            });

                KeyVaultTypes[] enumValues = (KeyVaultTypes[])Enum.GetValues(typeof(KeyVaultTypes));
                byte[] decryted;

                decryted = Convert.FromBase64String(GetKeyValue("clientId"));
                string clientId = System.Text.Encoding.Unicode.GetString(decryted);

                decryted = Convert.FromBase64String(GetKeyValue("clientSecret"));
                string clientSecret = System.Text.Encoding.Unicode.GetString(decryted);

                decryted = Convert.FromBase64String(GetKeyValue("tenantId"));
                string tenantId = System.Text.Encoding.Unicode.GetString(decryted);

                var vaultUri = new Uri(GetKeyValue("AzureKeyVaultUri"));

                ClientSecretCredential credential = new(tenantId, clientId, clientSecret);

                var client = new SecretClient(vaultUri, credential);

                foreach (var keyName in enumValues)
                {
                    string secret = KeyVaultManager.GetSettingValue(keyName);


                    if (!KeyVaultManager.IsPipelineVariableActive())
                    {
                        secret = client.GetSecret(secret).Value.Value;
                    }

                    KeyVaultManager.SetSecretValue(keyName.ToString(), secret);
                }        
        }

        /// <summary>
        /// Obtener valores variables de entorno
        /// </summary>
        public static string GetKeyValue(string value)
        {

            string? key = Environment.GetEnvironmentVariable(value);

            return key ?? "";
        }

        /// <summary>
        /// Configuración de inicio
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Habilitar Swagger en el entorno de desarrollo
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIMEM API");
                });
            }
            else
            {
                // Configuraciones para entorno de producción...
            }

            app.UseRouting();

            app.UseCors("AllowAngularApp");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Map("/backend-files", _app =>
            {
                _app.Run(async context =>
                {
                    // Create my object
                    var _object = new
                    {
                        Title = "Your web app is running and waiting for your content",
                        Description = "Your web app is live, but we don’t have your content yet. " +
                        "If you’ve already deployed, it could take up to 5 minutes for your content to show up, so come back soon.",
                        Code = "Supporting Node.js, Java, .NET and more"
                    };

                    // Transform it to JSON object
                    string jsonData = JsonConvert.SerializeObject(_object);
                    await context.Response.WriteAsync(jsonData);
                });
            });
        }
    }
}
