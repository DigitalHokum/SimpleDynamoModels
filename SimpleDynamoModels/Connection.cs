using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace SimpleDynamoModels
{
    public class Connection
    {
        public static readonly AmazonDynamoDBClient Client = new AmazonDynamoDBClient();
        public static readonly DynamoDBContext Context = new DynamoDBContext(Client);
    }
}
