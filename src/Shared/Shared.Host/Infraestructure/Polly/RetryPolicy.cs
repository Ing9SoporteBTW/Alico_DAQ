namespace Shared.Host.Infraestructure.Polly
{
    using global::Polly;
    using global::Polly.Extensions.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Polly;
    using System;
    using System.Net;
    using System.Net.Http;

    public static class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetPolicy<T>(IServiceProvider serviceProvider)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()

                .OrResult(message => message.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(
                    retryCount: 6,
                    sleepDurationProvider: retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        serviceProvider.GetService<ILogger<T>>()
                            .LogWarning("Retry by {message} with tracker {traker} .Delaying for {delay}ms, then making retry {retry}.", outcome.Exception.Message, outcome.Exception.StackTrace, timespan.TotalMilliseconds, retryAttempt);
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetPolicyWithJitterStrategy<T>(IServiceProvider serviceProvider)
        {
            var jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(message => message.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(
                    retryCount: 6,
                    sleepDurationProvider: retryAttemp =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)) +
                        TimeSpan.FromMilliseconds(jitterer.Next(0, 100)),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        serviceProvider.GetService<ILogger<T>>()
                            .LogWarning("Retry by {message} with tracker {traker} .Delaying for {delay}ms, then making retry {retry} with jitter strategy.", outcome.Exception == null ? "" : outcome.Exception.Message, outcome.Exception == null ? "" : outcome.Exception.StackTrace, timespan.TotalMilliseconds, retryAttempt);
                    });
        }
    }
}
