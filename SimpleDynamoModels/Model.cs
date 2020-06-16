using System;

namespace SimpleDynamoModels
{
    public abstract class Model<T>
    {
        public void Save()
        {
            
        }

        public void Delete()
        {
            
        }
        
        public static T Get(string id)
        {
            Type type = typeof(T);
            return default(T);
        }
        
        public static T Get(int id)
        {
            Type type = typeof(T);
            return default(T);
        }
    }
}
