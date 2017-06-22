using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess
{

	public class NullableDateConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var convertedValue =(DateTime?)value;
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
			if (reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString()) 
		 || reader.Value.ToString() == "-1" || reader.Value.ToString() == "All" )
			{
				return null;
			}

		    if (reader.Value.ToString() == "\"NULL\"" || reader.Value.ToString() == "01/01/0001")
			{
				return DateTime.MinValue;
			}

            if (reader.ValueType.ToString() == "System.String")
            {
                return DateTime.Parse(reader.Value.ToString());
            }

			return reader.Value;
		}
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime?);
		}
	}

}
