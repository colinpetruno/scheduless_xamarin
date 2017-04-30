using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scheduleless.Extensions;

namespace Scheduleless.Models.Converters
{
	public class JsonIntToDateTimeConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ((long)reader.Value).FromUnixTimeStamp();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dateTimeValue = (DateTime)value;

			JToken t = JToken.FromObject(dateTimeValue.ToUnixTimeStamp());

			if (t.Type != JTokenType.Object)
			{
				t.WriteTo(writer);
			}
			else
			{
				JObject o = (JObject)t;
				IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
				o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));
				o.WriteTo(writer);
			}
		}
	}
}
