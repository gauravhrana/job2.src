using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess
{

    public class NullableDecimalConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var convertedValue = (decimal?)value;
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
            if (reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString()) || reader.Value.ToString() == "-1" || reader.Value.ToString() == "All")
            {
                return null;
            }
            return decimal.Parse(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal?);
        }
    }

}
