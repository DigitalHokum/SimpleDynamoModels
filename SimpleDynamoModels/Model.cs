using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace SimpleDynamoModels
{
    public abstract class Model<T> where T : Model<T>
    {
        protected virtual void PreSave()
        {}

        public Task Save()
        {
            PreSave();
            return Connection.Context.SaveAsync((T) this);
        }

        public Task Delete()
        {
            object hashKey = GetDynamoHashKey();
            object rangeKey = GetDynamoRangeKey();

            if (rangeKey == null)
            {
                return Connection.Context.DeleteAsync<T>(hashKey);    
            }
      
            return Connection.Context.DeleteAsync<T>(hashKey, rangeKey);
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
        
        public object GetDynamoRangeKey()
        {
            Type type = typeof(T);
            foreach (FieldInfo field in type.GetFields())
            {
                DynamoDBRangeKeyAttribute[] fields = (DynamoDBRangeKeyAttribute[]) field.GetCustomAttributes(typeof(DynamoDBRangeKeyAttribute));
                if (fields.Length > 0)
                {
                    return field.GetValue(this);
                }
            }

            return null;
        }

        public static async Task<T> Get(object id)
        {
            return await Connection.Context.LoadAsync<T>(id);
        }
        
        public static async Task<List<T>> GetList(object id)
        {
            return await Connection.Context.QueryAsync<T>(id).GetRemainingAsync();
        }

        public static async Task<T> Get(object id, object sortKey)
        {
            return await Connection.Context.LoadAsync<T>(id, sortKey);
        }
    }
}
