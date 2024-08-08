using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;

public class BlobFunction
{
    private readonly ILogger<BlobFunction> _logger;
    private readonly BlobServiceClient _blobServiceClient;

    public BlobFunction(ILogger<BlobFunction> logger, BlobServiceClient blobServiceClient)
    {
        _logger = logger;
        _blobServiceClient = blobServiceClient;
    }

    [Function("ProcessBlob")]
    public async Task Run(
        [Blob("samples-workitems/{name}", Connection = "anggelick354storage")] Stream blobStream,
        string name)
    {
        using var reader = new StreamReader(blobStream);
        var content = await reader.ReadToEndAsync();
        _logger.LogInformation($"Blob function processed blob\n Name: {name} \n Data: {content}");
    }
}
