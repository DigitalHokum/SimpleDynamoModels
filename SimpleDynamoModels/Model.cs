using System;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace SimpleDynamoModels
{
    public abstract class Model<T>
    {
        public void Save()
        {
            Connection.Context.SaveAsync(this);
        }

        public void Delete()
        {
            Connection.Context.DeleteAsync<T>(GetDynamoHashKey());
        }

        public object GetDynamoHashKey()
        {
            Type type = typeof(T);
            foreach (FieldInfo field in type.GetFields())
            {
                DynamoDBHashKeyAttribute[] fields = (DynamoDBHashKeyAttribute[]) field.GetCustomAttributes(typeof(DynamoDBHashKeyAttribute));
                if (fields.Length > 0)
                {
                    return field.GetValue(this);
                }
            }

            return null;
        }

        public static async Task<T> Get(string id)
        {
            return await Connection.Context.LoadAsync<T>(id);
        }
        
        public static async Task<T> Get(int id)
        {
            return await Connection.Context.LoadAsync<T>(id);
        }
    }
}
