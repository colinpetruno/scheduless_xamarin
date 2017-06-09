using System;
using Newtonsoft.Json;
using Scheduleless.Models.Converters;

namespace Scheduleless.Models
{
    // {
    //  "id": 59,
    //  "user_location_id": 1,
    //  "company_id": 2,
    //  "minute_start": 780,
    //  "minute_end": 1080,
    //  "date": 20170430,
    //  "created_at": "2017-04-29T18:16:26.142Z",
    //  "updated_at": "2017-04-29T18:16:26.142Z"
    //}

    public class Trade
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("user_location_id")]
		public int UserLocationId { get; set; }

		[JsonProperty("company_id")]
		public int CompanyId { get; set; }

		[JsonProperty("minute_start")]
		public int MinuteStart { get; set; }

		[JsonProperty("minute_end")]
		public int MinuteEnd { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("created_at")]
		// FIXME: create a converter to convert the format of 2017-04-29T18:16:26.147Z
		//[JsonConverter(typeof(JsonIntToDateTimeConverter))]
		public string CreatedAt { get; set; }

		[JsonProperty("updated_at")]
		// FIXME: create a converter to convert the format of 2017-04-29T18:16:26.147Z
		//[JsonConverter(typeof(JsonIntToDateTimeConverter))]
		public string UpdatedAt { get; set; }

        public string Month {
            get {
                return "July";
            }
        }

        public string Day {
            get {
                return "10";
            }
        }

        public string Label {
            get {
                return $"{MinuteStart} - {MinuteEnd}";
            }
        }
	}
}
