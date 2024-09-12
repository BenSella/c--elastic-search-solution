using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchSolution.Utils.Interfaces
{
    /// <summary>
    /// Interface to define basic methods for interacting with Elasticsearch.
    /// Provides methods for indexing, retrieving, and searching documents in Elasticsearch.
    /// </summary>
    public interface IElasticSearchClient
    {
        /// <summary>
        /// Retrieves an instance of the Elasticsearch client.
        /// This method can be used to perform direct operations on the Elasticsearch client.
        /// </summary>
        /// <returns>An instance of the ElasticsearchClient.</returns>
        ElasticsearchClient GetClient();

        /// <summary>
        /// Asynchronously indexes a document into the specified Elasticsearch index.
        /// </summary>
        /// <typeparam name="T">The type of the document to be indexed.</typeparam>
        /// <param name="indexName">The name of the index where the document will be stored.</param>
        /// <param name="document">The document object to be indexed in Elasticsearch.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task IndexDocumentAsync<T>(string indexName, T document);

        /// <summary>
        /// Asynchronously retrieves a document from the specified Elasticsearch index by its ID.
        /// </summary>
        /// <typeparam name="T">The type of the document to retrieve.</typeparam>
        /// <param name="indexName">The name of the index where the document is stored.</param>
        /// <param name="documentId">The ID of the document to retrieve.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. The result contains the document if found, otherwise null.
        /// </returns>
        Task<T?> GetDocumentByIdAsync<T>(string indexName, string documentId);

        /// <summary>
        /// Asynchronously searches for documents in the specified Elasticsearch index based on a query string.
        /// </summary>
        /// <typeparam name="T">The type of the document to search for.</typeparam>
        /// <param name="indexName">The name of the index where the documents are stored.</param>
        /// <param name="query">The query string to search for documents in Elasticsearch.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. The result contains the search response and matching documents.
        /// </returns>
        Task<SearchResponse<T>> SearchDocumentsAsync<T>(string indexName, string query);
    }
}
