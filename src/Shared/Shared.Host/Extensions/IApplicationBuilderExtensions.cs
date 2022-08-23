namespace Shared.Host.Extensions
{
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using SharedService.Responses.HealthCheck;
    using System.Linq;

    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomHealthchecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) => {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Componenet = x.Key,
                            status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        Duration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

                }
            })
            ;
            app.UseHealthChecksUI();

            return app.UseHealthChecks("/ready", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains("dependencies")
            });
        }

        public static IApplicationBuilder UseCustomProblemDetail(this IApplicationBuilder app)
        {
            return app.UseProblemDetails();
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, string clientid)
        {
            return app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            })
          .UseSwaggerUI(c =>
          {
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "Capa de negocios");
              c.OAuthClientId(clientid);
              c.OAuthAppName("Swagger UI");
              c.RoutePrefix = string.Empty;

          });
        }
    }
}
