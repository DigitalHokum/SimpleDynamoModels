using System.Threading.Tasks;
using SimpleDynamoModels.Test.Models;
using Xunit;

namespace SimpleDynamoModels.Test
{
    public class ModelTests
    {
        [Fact]
        public async Task GetHashKey()
        {
            string hashKeyValue = "hashkey";
            TestModel m = new TestModel()
            {
                TestString = hashKeyValue
            };
            
            Assert.Equal(hashKeyValue, m.GetDynamoHashKey());
        }
    }
}
