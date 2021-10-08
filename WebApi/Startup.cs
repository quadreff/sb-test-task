using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SBTestTask.Common.Infrastructure;
using SBTestTask.Common.Infrastructure.Mongo;
using SBTestTask.Common.Infrastructure.RabbitMq;
using SBTestTask.Common.Logging;
using SBTestTask.WebApi.App.Validation;
using SBTestTask.WebApi.Helpers.RabbitMq;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;
using Serilog;
using Serilog.Core;
using Log = SBTestTask.Common.Logging.Log;

namespace SBTestTask.WebApi
{
    public class Startup
    {
        private readonly LoggerConfiguration _logConfiguration = new LoggerConfiguration();
        private Logger? _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            SetupLogger();
            SetupDi(services);
            SetupJwtAuth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void SetupJwtAuth(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var tokenSpecs = serviceProvider.GetRequiredService<IJwtConfiguration>().Get();
            var key = new SymmetricSecurityKey(Convert.FromBase64String(tokenSpecs.Secret));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // disabled for dev purposes
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = tokenSpecs.Issuer,
                        ValidateAudience = true,
                        ValidAudience = tokenSpecs.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = key,
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        private static void SetupDi(IServiceCollection services)
        {
            services.AddScoped<IJwtConfiguration, JwtConfiguration>();
            services.AddScoped<IJwtTokenManager, JwtTokenManager>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IRabbitMqConfiguration, RabbitMqConfiguration>();
            services.AddSingleton<IRabbitQueue, RabbitQueue>();
            services.AddScoped<IMongoDbConfiguration, MongoDbConfiguration>();
            services.AddScoped<IMongoDbContext, MongoDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private void SetupLogger()
        {
            _logger = _logConfiguration
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("webapi.log")
                .CreateLogger();

            // Depend upon log abstraction
            Log.Setup((severity, s) =>
            {
                switch (severity)
                {
                    case LogSeverity.Error:
                        _logger.Error(s);
                        break;
                    case LogSeverity.Info:
                        _logger.Information(s);
                        break;
                    case LogSeverity.Trace:
                        _logger.Debug(s);
                        break;
                    default: throw new ArgumentException("Invalid log level");
                }
            });
        }
    }
}