namespace SimpleDynamoModels.Test.Models
{
    [RegisterModel("Test")]
    public class TestModel : Model<TestModel>
    {
        [Field] public string TestString;
    }
}
