using Amazon.DynamoDBv2.DataModel;

namespace SimpleDynamoModels.Test.Models
{
    [DynamoDBTable("Test")]
    public class TestModel : Model<TestModel>
    {
        [DynamoDBHashKey] public string TestString;
    }
}
