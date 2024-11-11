using Polly.Extensions.Http;
using Polly;

namespace Infrastructure.Common;

public static class RetryPolicy
{
    private const int RetryCount = 6;

    private static readonly Random Random = new();

    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode is
                System.Net.HttpStatusCode.TooManyRequests or
                System.Net.HttpStatusCode.InternalServerError or
                System.Net.HttpStatusCode.Unauthorized)
            .WaitAndRetryAsync(RetryCount, retryAttempt =>
            {
                // Exponential backoff
                var baseDelay = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));

                // Add a random jitter (between 0 and 1 second) to make requests more random in time in case of high concurrency
                var jitter = TimeSpan.FromMilliseconds(Random.Next(0, 1000));

                // Return the final delay with jitter
                return baseDelay + jitter;
            });
    }
}