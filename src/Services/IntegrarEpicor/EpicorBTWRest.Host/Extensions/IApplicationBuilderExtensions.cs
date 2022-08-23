using Microsoft.AspNetCore.Builder;

namespace EpicorBTWRest.Host.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        =>
            app.UseSwagger()
            .UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicio 2");
            });

    }
}
