
using EpicorBTWRest.Host;
using Microsoft.EntityFrameworkCore;
using Serilog;

//SERVICIOS
var builder = WebApplication.CreateBuilder(args);

//INICIAR EL LOG
builder.Host.UseSerilog((context, services, configuration)
    => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
    );
HostExtensions.ConfigurationService(builder.Services, builder.Configuration);

SharedService.Functions.Helpers.APIPrefix = builder.Configuration["APIPrefix"];

//HOST
var app = builder.Build();
HostExtensions.Configure(app, host =>
{
    return host.UseDefaultFiles().UseStaticFiles();
});


//INICIO DEL PROYECTO
app.Run();

