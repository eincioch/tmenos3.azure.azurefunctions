using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TMenos3.Azure.AzureFunctions.Demos.ScheduledTasks
{
    public static class CleanUpStorage
    {
        [FunctionName("CleanUpStorage")]
        public static async Task Run(
            [TimerTrigger("* * * * *")]
                TimerInfo myTimer,
            ILogger logger)
        {
            var connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("pending");

            BlobContinuationToken blobContinuationToken = null;
            var blobList = await cloudBlobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
            var cloudBlobList = blobList.Results.Select(blb => blb as ICloudBlob);

            foreach (var item in cloudBlobList)
            {
                await item.DeleteIfExistsAsync();
            }
        }
    }
}
