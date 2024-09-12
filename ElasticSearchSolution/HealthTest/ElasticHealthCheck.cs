using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace YourNamespace.HealthTest
{
    public class ElasticHealthCheck : IHealthCheck
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        // Inject the Elasticsearch client into the health check class
        public ElasticHealthCheck(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        // The HealthCheck method to evaluate the health of Elasticsearch
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Ping the Elasticsearch server to check if it's responsive
                var pingResponse = await _elasticsearchClient.PingAsync();

                if (pingResponse.IsValidResponse)
                {
                    return HealthCheckResult.Healthy("Elasticsearch is healthy.");
                }

                return HealthCheckResult.Unhealthy("Elasticsearch is not responding.");
            }
            catch (Exception ex)
            {
                // Return Unhealthy if an exception is caught
                return HealthCheckResult.Unhealthy($"Elasticsearch check failed: {ex.Message}");
            }
        }
    }
}