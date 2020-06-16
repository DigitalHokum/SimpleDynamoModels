using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace SimpleDynamoModels
{
    public class Connection
    {
        private static AmazonDynamoDBClient _client;
        private static DynamoDBContext _context;

        public static AmazonDynamoDBClient Client => GetClient();
        public static DynamoDBContext Context => GetContext();

        
        public static AmazonDynamoDBClient GetClient()
        {
            if (_client == null)
            {
                _client = new AmazonDynamoDBClient();
            }

            return _client;
        }

        public static DynamoDBContext GetContext()
        {
            if (_context == null)
            {
                _context = new DynamoDBContext(Client, new DynamoDBContextConfig
                {
                    ConsistentRead = true
                });
            }

            return _context;
        }
    }
}
