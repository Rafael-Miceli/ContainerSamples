using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace data
{
    //Docs para usar Dynamo: 
    //https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/dynamodb-intro.html
    //https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/dynamodb-intro.html#dynamodb-intro-apis-object-persistence
    public class ClientDynamoRepo
    {
        public async Task Insert(Client client) 
        {
            var Dbclient = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(Dbclient);

            await context.SaveAsync(client);
        }
            
    }
}
