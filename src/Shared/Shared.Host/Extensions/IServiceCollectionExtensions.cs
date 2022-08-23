namespace Microsoft.Extensions.DependencyInjection
{
    using Shared.Host.Diagnostics;
    using Shared.Host.Infraestructure.Middleware;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.OpenApi.Models;
    using System.IO;
    using System.Reflection;
    using SharedService.Models;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services) =>
        services
            .AddMvc()
            .AddApplicationPart(typeof(IServiceCollectionExtensions).Assembly)
            //.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .Services;

        public static IServiceCollection AddCustomMiddlewares(this IServiceCollection services) =>
        services
            .AddScoped<ErrorMiddleware>();

        public static IServiceCollection AddCustomHealthChecks<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var hcBuilder = services.AddHealthChecks();
            return services;
        }

        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services) =>
           services
               .AddProblemDetails(configure =>
               {
                   configure.IncludeExceptionDetails = (ctx, ex) =>
                   {
                       var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                       return env.IsDevelopment() || env.IsStaging();
                   };
               });

        public static IServiceCollection AddCustomApiBehaviour(this IServiceCollection services)
        {

            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
                options.SuppressInferBindingSourcesForParameters = false;

                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Detail = "Please refer to the errors property for additional details."
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes =
                         {
                                "application/problem+json",
                                "application/problem+xml"
                         }
                    };
                };
            });
        }


        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, CustomAuthorization customAuthorization)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = customAuthorization.UrlIdentity;
                options.RequireHttpsMetadata = false;
                options.Audience = customAuthorization.Audience;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PublicSecure", policy => policy.RequireClaim("client_id", customAuthorization.ClientId));
            });
            return services;
        }

        public static IServiceCollection AddHostingDiagnosticHandler(this IServiceCollection services)
        {
            return services.AddHostedService<HostingDiagnosticHandler>();
        }

        public static IServiceCollection AddCustomHttContext(this IServiceCollection services)
        {
            return services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, CustomSwagger customSwagger)
        {
            return services.AddSwaggerGen(c =>
            {
                //c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Capa de negocios", Version = "v1.0.0" });
                c.TagActionsBy(api => new[] { api.GroupName });
                c.DocInclusionPredicate((name, api) => true);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Flows = new OpenApiOAuthFlows()
                    {
                        ClientCredentials = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{customSwagger.UrlIdentity}/connect/authorize"),
                            TokenUrl = new Uri($"{customSwagger.UrlIdentity}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                                {
                                    { customSwagger.Key, customSwagger.Value }
                                }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
}
