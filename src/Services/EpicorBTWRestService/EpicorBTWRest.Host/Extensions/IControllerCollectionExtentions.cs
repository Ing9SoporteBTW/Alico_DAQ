namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using EpicorBTWRest.Rules.Repositories;
    using EpicorBTWRest.Rules.Services;
    using Shared.Host.Infraestructure.Polly;

    public static class IControllerCollectionExtentions
    {
        public static IServiceCollection AddBAQService(this IServiceCollection services) =>
            services.
                AddHttpClient<IBAQService, BAQService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<BAQService>(serviceProvider))
                .Services;
        public static IServiceCollection AddLoginService(this IServiceCollection services) =>
            services.
                AddHttpClient<ILoginService, LoginService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<LoginService>(serviceProvider))
                .Services;
        public static IServiceCollection AddLaborDtlService(this IServiceCollection services) =>
           services.
               AddHttpClient<ILaborDtlService, LaborDtlService >()
               .SetHandlerLifetime(TimeSpan.FromMinutes(5))
               .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<LaborDtlService>(serviceProvider))
               .Services;
        public static IServiceCollection AddUD05Service(this IServiceCollection services) =>
           services.
               AddHttpClient<IUD05Service, UD05Service>()
               .SetHandlerLifetime(TimeSpan.FromMinutes(5))
               .AddPolicyHandler((serviceProvider, request) => RetryPolicy.GetPolicyWithJitterStrategy<UD05Service>(serviceProvider))
               .Services;
    }
}
