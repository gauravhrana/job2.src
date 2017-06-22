using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess
{

    public class NullableIntConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var convertedValue = (int?)value;
            if (convertedValue != null)
            {
                writer.WriteValue(convertedValue.Value.ToString());
            }
            else
            {
                writer.WriteValue(string.Empty);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString().Trim()) || reader.Value.ToString().Trim() == "-1" || reader.Value.ToString().Trim() == "All")
            {
                return null;
            }
            return int.Parse(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int?);
        }
    }

}
