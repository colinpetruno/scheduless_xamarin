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

		/// <summary>
		/// When deserializing, this attempts to convert an object to a DateTime object.
		/// </summary>
		/// <returns>The json.</returns>
		/// <param name="reader">Reader.</param>
		/// <param name="objectType">Object type.</param>
		/// <param name="existingValue">Existing value.</param>
		/// <param name="serializer">Serializer.</param>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value is DateTime)
			{
				return reader.Value;
			}

			var stringValue = reader.Value.ToString();
			var convertedDateTime = DateTime.ParseExact(stringValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
			return convertedDateTime;
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
