using Hellang.Middleware.ProblemDetails;
using EpicorBTWRest.Host.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EpicorBTWRest.Host
{
    public static class HostExtensions
    {
        readonly static string SpecificOrigins = "_specificOrigins";
        public static IServiceCollection ConfigurationService(IServiceCollection services, IConfiguration configuration)
        {
            return
                services.AddCustomerMvc()
                        .AddCustomerProblemDetail()
                        .AddCustomerSwagger()
                        .AddCustomEntityFramewrok(configuration)
                        #region SERVICIO DE CONTROLADORES
                        .AddLoginService()
                        .AddLaborService()
                        .AddUD05Service()

            #endregion
                        .AddHttpContextAccessor()
                        .AddCors(c => {
                            c.AddPolicy(name: SpecificOrigins, builder =>
                            {
                                builder
                               .AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                            });
                        });
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configurHost)
        {
            return configurHost(app)
                    .UseProblemDetails()
                    .UseHttpsRedirection()
                    .UseAuthorization()
                    .UseRouting()
                    .UseCustomSwagger()
                    .UseEndpoints(endpoints =>
                    {
                        //endpoints.MapControllers();

                        endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                        //endpoints.MapRazorPages();
                    });
        }

    }
}
