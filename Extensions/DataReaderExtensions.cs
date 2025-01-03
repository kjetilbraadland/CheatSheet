using System.Data;
using System.Reflection;

namespace Extensions
{
    public static class DataReaderExtensions
    {
        public static List<T> MapDataReaderToList<T>(this IDataReader dataReader) where T : new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (dataReader.Read())
            {
                var item = new T();
                foreach (var property in properties)
                {
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal(property.Name)))
                    {
                        var value = dataReader.GetValue(dataReader.GetOrdinal(property.Name));
                        property.SetValue(item, value);
                    }
                }
                result.Add(item);
            }

            return result;
        }
    }
}