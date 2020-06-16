using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleDynamoModels
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterModel : Attribute
    {
        public static readonly Dictionary<string, RegisterModel> Models = new Dictionary<string, RegisterModel>();
        private Type _type;

        public Type Type => _type;
        public readonly string TableName;
        public readonly Dictionary<FieldInfo, Field> Fields = new Dictionary<FieldInfo, Field>();

        public RegisterModel(string tableName)
        {
            TableName = tableName;
        }
        
        public void Register(Type type)
        {
            _type = type;
        }
        
        public void RegisterField(FieldInfo info, Field field)
        {
            Fields.Add(info, field);
        }
        
        public static void Discover()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    RegisterModel[] _models = (RegisterModel[]) type.GetCustomAttributes(typeof(RegisterModel), false);
                    if (_models.Length > 0)
                    {
                        foreach (RegisterModel model in _models)
                        {
                            model.Register(type);
                            Models.Add(model.TableName, model);

                            foreach (FieldInfo field in type.GetFields())
                            {
                                Field[] fields = (Field[]) field.GetCustomAttributes(typeof(Field));
                                if (fields.Length > 0)
                                {
                                    foreach (Field _field in fields)
                                    {
                                        model.RegisterField(field, _field);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static RegisterModel GetForObject<T>(Model<T> model)
        {
            Type type = model.GetType();

            foreach (RegisterModel registerModel in Models.Values)
            {
                if (registerModel.Type == type)
                    return registerModel;
            }

            return null;
        }
    }
}
