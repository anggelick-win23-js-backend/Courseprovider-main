using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;

namespace CourseProvider
{
    public class Function
    {
        private readonly ILogger<Function> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public Function(ILogger<Function> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        [Function("ProcessBlob")]
        public async Task Run(
            [BlobTrigger("samples-workitems/{name}", Connection = "VaultUri")] Stream blobStream,
            string name)
        {
            using var blobStreamReader = new StreamReader(blobStream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
