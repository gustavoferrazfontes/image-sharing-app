// See https://aka.ms/new-console-template for more information

using Azure;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>(); // Add this line to include user secrets
//IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
IConfigurationRoot configuration = builder.Build();

var searchServiceUri = configuration["SearchServiceUri"];
var searchServiceApiKey = configuration["SearchServiceApiKey"];
var indexName = "created-users-index";

var searchIndexClient = new SearchIndexClient(new Uri(searchServiceUri), new AzureKeyCredential(searchServiceApiKey));
var searchIndexerClient = new SearchIndexerClient(new Uri(searchServiceUri), new AzureKeyCredential(searchServiceApiKey));

try
{
    await searchIndexClient.GetIndexAsync(indexName);
    await searchIndexClient.DeleteIndexAsync(indexName);
}
catch (RequestFailedException ex) when (ex.Status == 404)
{
    // if the index doesn't exist, throw 404
}

var fieldBuilder = new FieldBuilder();
var definition = new SearchIndex(indexName, fieldBuilder.Build(typeof(CreatedUser)));
await searchIndexClient.CreateIndexAsync(definition);

var blobDataSource = new SearchIndexerDataSourceConnection(
    name: configuration["BlobStorageAccountName"],
    type: SearchIndexerDataSourceType.AzureBlob,
    connectionString: configuration["BlobStorageConnectionString"],
    container: new SearchIndexerDataContainer("users-search"));

await searchIndexerClient.CreateOrUpdateDataSourceConnectionAsync(blobDataSource);


var indexingParameters = new IndexingParameters()
{
    IndexingParametersConfiguration = new IndexingParametersConfiguration()
};
indexingParameters.IndexingParametersConfiguration.Add("parsingMode", "json");

SearchIndexer blobIndexer = new SearchIndexer(name: "created-users-blob-indexer", dataSourceName: blobDataSource.Name, targetIndexName: indexName)
{
    Parameters = indexingParameters,
    Schedule = new IndexingSchedule(TimeSpan.FromDays(1))
};


try
{
    await searchIndexerClient.GetIndexerAsync(blobIndexer.Name);
    await searchIndexerClient.ResetIndexerAsync(blobIndexer.Name);
}
catch (RequestFailedException ex) when (ex.Status == 404) { }

await searchIndexerClient.CreateOrUpdateIndexerAsync(blobIndexer);

try
{
    await searchIndexerClient.RunIndexerAsync(blobIndexer.Name);
}
catch (RequestFailedException ex) when (ex.Status == 429)
{
    Console.WriteLine($"Failed to run indexer: {ex.Message}");
    //throw;
}