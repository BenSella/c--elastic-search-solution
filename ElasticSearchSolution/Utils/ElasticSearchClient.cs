using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ElasticSearchSolution.Utils.Interfaces;

namespace ElasticSearchSolution.Utils
{
    /// <summary>
    /// This class provides an implementation of the IElasticSearchClient interface,
    /// allowing interaction with Elasticsearch via the Elastic.Clients.Elasticsearch package.
    /// </summary>
    public class ElasticSearchClient : IElasticSearchClient
    {
        private readonly ElasticsearchClient _client;

        /// <summary>
        /// Initializes a new instance of the ElasticSearchClient class.
        /// It sets up the Elasticsearch client using configuration values for URI, username, and password.
        /// </summary>
        /// <param name="configuration">
        /// The configuration object containing Elasticsearch connection details, including the URI, 
        /// username, and password.
        /// </param>
        public ElasticSearchClient(IConfiguration configuration)
        {
            // Setup Elasticsearch settings with authentication
            var settings = new ElasticsearchClientSettings(new Uri(configuration["Elasticsearch:Uri"]))
                .Authentication(new BasicAuthentication(configuration["Elasticsearch:Username"], configuration["Elasticsearch:Password"]));

            // Create an Elasticsearch client using the settings
            _client = new ElasticsearchClient(settings);
        }

        /// <summary>
        /// Retrieves the current instance of the Elasticsearch client.
        /// This method allows other parts of the system to use the client directly.
        /// </summary>
        /// <returns>An instance of the ElasticsearchClient.</returns>
        public ElasticsearchClient GetClient()
        {
            return _client;
        }

        /// <summary>
        /// Asynchronously indexes a document into Elasticsearch.
        /// </summary>
        /// <typeparam name="T">The type of the document to be indexed.</typeparam>
        /// <param name="indexName">The name of the index where the document will be stored.</param>
        /// <param name="document">The document object to be indexed in Elasticsearch.</param>
        /// <exception cref="Exception">Throws an exception if the indexing operation fails.</exception>
        public async Task IndexDocumentAsync<T>(string indexName, T document)
        {
            var response = await _client.IndexAsync(document, idx => idx.Index(indexName));

            // Validate the response to ensure the indexing operation was successful
            if (!response.IsValidResponse)
            {
                throw new Exception($"Failed to index document: {response.DebugInformation}");
            }
        }

        /// <summary>
        /// Asynchronously retrieves a document from Elasticsearch by its ID.
        /// </summary>
        /// <typeparam name="T">The type of the document to retrieve.</typeparam>
        /// <param name="indexName">The name of the index where the document is stored.</param>
        /// <param name="documentId">The ID of the document to retrieve.</param>
        /// <returns>The document object if found, otherwise null.</returns>
        /// <exception cref="Exception">Throws an exception if the retrieval operation fails.</exception>
        public async Task<T?> GetDocumentByIdAsync<T>(string indexName, string documentId)
        {
            var response = await _client.GetAsync<T>(documentId, idx => idx.Index(indexName));

            // Validate the response to ensure the document retrieval operation was successful
            if (!response.IsValidResponse)
            {
                throw new Exception($"Failed to get document by ID: {response.DebugInformation}");
            }

            return response.Source;
        }

        /// <summary>
        /// Asynchronously searches for documents in Elasticsearch based on a query string.
        /// </summary>
        /// <typeparam name="T">The type of the document to search for.</typeparam>
        /// <param name="indexName">The name of the index where the documents are stored.</param>
        /// <param name="query">The query string to search for documents in Elasticsearch.</param>
        /// <returns>A search response object containing the matching documents.</returns>
        /// <exception cref="Exception">Throws an exception if the search operation fails.</exception>
        public async Task<SearchResponse<T>> SearchDocumentsAsync<T>(string indexName, string query)
        {
            var searchResponse = await _client.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(q => q
                    .QueryString(qs => qs.Query(query))
                ));

            // Validate the response to ensure the search operation was successful
            if (!searchResponse.IsValidResponse)
            {
                throw new Exception($"Search query failed: {searchResponse.DebugInformation}");
            }

            return searchResponse;
        }
    }
}
