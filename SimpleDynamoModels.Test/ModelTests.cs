using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleDynamoModels.Test.Models;
using Xunit;

namespace SimpleDynamoModels.Test
{
    public class ModelTests
    {
        public ModelTests()
        {
            RegisterModel.Discover();
        }

        [Fact]
        public async Task GetModelForTable()
        {
            RegisterModel registerModel = RegisterModel.GetForTable("Test");
            Assert.Equal(typeof(TestModel), registerModel.Type);
        }
        
        [Fact]
        public async Task GetModelFieldList()
        {
            RegisterModel registerModel = RegisterModel.GetForTable("Test");
            List<string> fields = registerModel.GetFieldNames();
            
            Assert.Contains("TestString", fields);
        }
    }
}
