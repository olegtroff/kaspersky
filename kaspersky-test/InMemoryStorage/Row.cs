using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InMemoryStorage
{
    public class Row
    {
        public Row()
        {
            Data = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Data { get; }

        private static IEnumerable<PropertyInfo> GetProps(Type type)
        {
            return type.GetProperties().Where(prop => prop.CanWrite && prop.CanRead);
        }

        public void Merge(object instance)
        {
            foreach (var prop in GetProps(instance.GetType()))
            {
                var value = prop.GetValue(instance, null);
                if (Data.ContainsKey(prop.Name))
                {
                    if (value == null)
                        Data.Remove(prop.Name);
                    else
                        Data[prop.Name] = value;
                }
                else
                {
                    if (value != null)
                        Data.Add(prop.Name, value);
                }
            }
        }


        public void Replace(object instance)
        {
            Data.Clear();
            foreach (var prop in GetProps(instance.GetType()))
            {
                var value = prop.GetValue(instance, null);
                if (value != null) Data.Add(prop.Name, value);
            }
        }

        public static Row Serialize(object instance)
        {
            var result = new Row();
            result.Replace(instance);
            return result;
        }

        public T Deserialize<T>() where T : new()
        {
            var result = new T();
            foreach (var prop in GetProps(result.GetType()).Where(prop => Data.ContainsKey(prop.Name)))
                prop.SetValue(result, Data[prop.Name], null);

            return result;
        }
    }
}
