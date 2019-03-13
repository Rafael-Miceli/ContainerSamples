using Amazon.DynamoDBv2.DataModel;

namespace app_clients_processor
{
    [DynamoDBTable("AnimalsInventory")]
    public class Client
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}