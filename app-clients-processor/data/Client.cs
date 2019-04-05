using Amazon.DynamoDBv2.DataModel;

namespace data
{
    [DynamoDBTable("Client")]
    public class Client
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}