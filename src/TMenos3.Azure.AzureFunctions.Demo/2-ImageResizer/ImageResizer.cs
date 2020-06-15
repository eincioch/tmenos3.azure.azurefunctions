using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace TMenos3.Azure.AzureFunctions.Demos.ImageResizer
{
    public static class ImageResizer
    {
        [FunctionName("ImageResizer")]
        public static async Task Run(
            [BlobTrigger("pending/{name}", Connection = "StorageConnectionString")]
                Stream input,
            [Blob("small/{name}", FileAccess.Write, Connection = "StorageConnectionString")]
                Stream small,
            [Blob("medium/{name}", FileAccess.Write, Connection = "StorageConnectionString")]
                Stream medium,
                ILogger logger)
        {
            var smallStream = await ResizeToSmallImageAsync(input);
            await smallStream.CopyToAsync(small);

            var mediumStream = await ResizeToMediumImageAsync(input);
            await mediumStream.CopyToAsync(medium);
        }

        private static Task<Stream> ResizeToSmallImageAsync(Stream toResize)
        {
            // Your magic goes here
            toResize.Position = 0;
            return Task.FromResult(toResize);
        }

        private static Task<Stream> ResizeToMediumImageAsync(Stream toResize)
        {
            // Your magic goes here
            toResize.Position = 0;
            return Task.FromResult(toResize);
        }
    }
}
