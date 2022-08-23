using EpicorBTWRest.DataAccess.DataContext;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EpicorBTWRest.Host.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddCustomerProblemDetail(this IServiceCollection services) =>
            services.AddProblemDetails(configure =>
            {
                configure.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
            });

        public static IServiceCollection AddCustomerSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Capa de negocios", Version = "v1.0.0" });
                c.TagActionsBy(api => new[] { api.GroupName });
                c.DocInclusionPredicate((name, api) => true);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


        public static IServiceCollection AddCustomerMvc(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }

        public static IServiceCollection AddCustomEntityFramewrok(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IntermediaContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("default"));
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            return services;
        }
    }
}
