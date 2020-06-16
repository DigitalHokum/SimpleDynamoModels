using System;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace SimpleDynamoModels
{
    public abstract class Model<T> where T : Model<T>
    {
        public Task Save()
        {
            return Connection.Context.SaveAsync((T) this);
        }

        public Task Delete()
        {
            return Connection.Context.DeleteAsync<T>(GetDynamoHashKey());
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

        public static Task<T> Get(object id)
        {
            return Connection.Context.LoadAsync<T>(id);
        }
    }
}
