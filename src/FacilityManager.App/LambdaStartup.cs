using FacilityManager.App.Authentication;
using FacilityManager.App.BackgroundServices;
using FacilityManager.App.Startup;
using FacilityManager.Application.Core;
using FacilityManager.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FacilityManager.App;

public sealed class LambdaStartup
{
    public LambdaStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(AssemblyReference.Assembly);

        services.AddAuthentication(options => { options.DefaultScheme = "ApiKey"; })
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", _ => { });

        services.AddAuthorizationBuilder()
            .AddPolicy("ApiKeyPolicy", policy => policy.RequireAuthenticatedUser());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "ApiKey",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    []
                }
            });
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.RegisterRepositories();
        services.RegisterServices();

        services.AddAutoMapper(options =>
            options.AddMaps(Application.AssemblyReference.Assembly));

        services.AddHostedService<QueuedHostedService>();

        services.AddSingleton<IBackgroundTaskQueue>(_ => new BackgroundTaskQueue(100));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers()
                .RequireAuthorization();
        });
    }
}