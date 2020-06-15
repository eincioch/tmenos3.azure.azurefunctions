using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TMenos3.Azure.AzureFunctions.Demos.FileImporter
{
    public static class FileImporterAsync
    {
        [FunctionName("FileImporterAsync")]
        public static async Task Run(
            [BlobTrigger("customers/{customerId}-{orderType}.txt", Connection = "StorageConnectionString")]
                Stream input,
            string customerId,
            string orderType,
            [Table("Orders", Connection = "StorageConnectionString")]
                IAsyncCollector<Order> ordersOutput,
            ILogger logger)
        {
            var orderTasks = (await ReadOrdersAsync(customerId, orderType, input))
                .Select(order => ordersOutput.AddAsync(order));

            await Task.WhenAll(orderTasks);

            await ordersOutput.FlushAsync();
        }

        private static async Task<IEnumerable<Order>> ReadOrdersAsync(string customerId, string orderType, Stream input)
        {
            using var streamReader = new StreamReader(input);
            var lines = await streamReader.ReadToEndAsync();

            return lines
                .Split("\n")
                .Select((product, index) => new Order
                {
                    PartitionKey = customerId,
                    RowKey = index.ToString(),
                    OrderType = orderType,
                    Product = product
                })
                .ToList();
        }
    }
}
