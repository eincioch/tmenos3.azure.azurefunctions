using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TMenos3.Azure.AzureFunctions.Demos.FileImporter
{
    public static class FileImporter
    {
        [FunctionName("FileImporter")]
        public static void Run(
            [BlobTrigger("customers/{customerId}-{orderType}.txt", Connection = "StorageConnectionString")]
                Stream input,
            string customerId,
            string orderType,
            [Table("Orders", Connection = "StorageConnectionString")]
                ICollector<Order> ordersOutput,
            ILogger logger)
        {
            var orders = ReadOrders(customerId, orderType, input);

            foreach (var order in orders)
                ordersOutput.Add(order);
        }

        private static IEnumerable<Order> ReadOrders(string customerId, string orderType, Stream input)
        {
            using var streamReader = new StreamReader(input);
            return streamReader
                .ReadToEnd()
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
