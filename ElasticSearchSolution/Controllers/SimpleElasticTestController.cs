using Microsoft.AspNetCore.Mvc;
using ElasticSearchSolution.Utils.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ElasticsearchController : ControllerBase
{
    private readonly IElasticSearchClient _elasticSearchClient;

    /// <summary>
    /// Initializes a new instance of the ElasticsearchController class with the provided IElasticSearchClient.
    /// </summary>
    /// <param name="elasticSearchClient">The Elasticsearch client to interact with Elasticsearch services.</param>
    public ElasticsearchController(IElasticSearchClient elasticSearchClient)
    {
        _elasticSearchClient = elasticSearchClient;
    }

    /// <summary>
    /// Indexes a document into the Elasticsearch index "my-index".
    /// </summary>
    /// <param name="document">The document object to be indexed in Elasticsearch.</param>
    /// <returns>An IActionResult indicating the success of the indexing operation.</returns>
    [HttpPost("index")]
    public async Task<IActionResult> IndexDocument([FromBody] object document)
    {
        // Index the document asynchronously into "my-index"
        await _elasticSearchClient.IndexDocumentAsync("my-index", document);
        return Ok("Document indexed successfully.");
    }

    /// <summary>
    /// Retrieves a document from Elasticsearch by its ID.
    /// </summary>
    /// <param name="id">The ID of the document to retrieve.</param>
    /// <returns>An IActionResult containing the retrieved document.</returns>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetDocument(string id)
    {
        // Retrieve the document by ID from "my-index"
        var document = await _elasticSearchClient.GetDocumentByIdAsync<object>("my-index", id);
        return Ok(document);  // Return the document in the response
    }

    /// <summary>
    /// Searches for documents in Elasticsearch based on the provided query string.
    /// </summary>
    /// <param name="query">The query string used to search the Elasticsearch index.</param>
    /// <returns>An IActionResult containing the search results.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchDocuments([FromQuery] string query)
    {


