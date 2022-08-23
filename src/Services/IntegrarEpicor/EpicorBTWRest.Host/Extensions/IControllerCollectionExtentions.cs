namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using EpicorBTWRest.Rules.Repositories;
    using EpicorBTWRest.Rules.Services;
    using Shared.Host.Infraestructure.Polly;

    public static class IControllerCollectionExtentions
    {
        
        public static IServiceCollection AddLoginService(this IServiceCollection services) =>
            services.
                AddHttpClient<ILoginService, LoginService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<LoginService>(serviceProvider))
                .Services;
        public static IServiceCollection AddLaborService(this IServiceCollection services) =>
           services.
               AddHttpClient<ILaborService, LaborService >()
               .SetHandlerLifetime(TimeSpan.FromMinutes(5))
               .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<LaborService>(serviceProvider))
               .Services;
        public static IServiceCollection AddUD05Service(this IServiceCollection services) =>
          services.
               AddHttpClient<IUD05Service, UD05Service>()
              .SetHandlerLifetime(TimeSpan.FromMinutes(5))
              .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<UD05Service>(serviceProvider))
              .Services;

    }
}
