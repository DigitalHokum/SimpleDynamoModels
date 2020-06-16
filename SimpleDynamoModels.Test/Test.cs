using System.Threading.Tasks;
using Xunit;

namespace SimpleDynamoModels.Test
{
    public class DataTest
    {
        [Fact]
        public async Task ModelRegistration()
        {
            RegisterModel.Discover();
        
            Assert.Single(RegisterModel.Models);
        }
    }
}
