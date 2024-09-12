using Elastic.Clients.Elasticsearch;
using ElasticSearchSolution.Utils;
using ElasticSearchSolution.Utils.Interfaces;
using YourNamespace.HealthTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IElasticSearchClient, ElasticSearchClient>();
// health test

builder.Services.AddSingleton(new ElasticsearchClient(
    new ElasticsearchClientSettings(new Uri(builder.Configuration["Elasticsearch:Uri"]))));
builder.Services.AddHealthChecks()
    .AddCheck<ElasticHealthCheck>("elasticsearch");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//health test
app.MapHealthChecks("/health");

app.Run();
