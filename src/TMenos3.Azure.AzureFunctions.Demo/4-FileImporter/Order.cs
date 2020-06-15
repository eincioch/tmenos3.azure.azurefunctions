namespace TMenos3.Azure.AzureFunctions.Demos.FileImporter
{
    public class Order
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string OrderType { get; set; }

        public string Product { get; set; }
    }
}
