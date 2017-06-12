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

    public class FutureShift
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("city_state_zip")]
        public string CityStateZip { get; set; }

        [JsonProperty("checked_in")]
        public Boolean CheckedIn { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("short_month")]
        public string ShortMonth { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("location_name")]
        public string LocationName { get; set; }

        [JsonProperty("location_line_1")]
        public string LocationLine1 { get; set; }

        [JsonProperty("location_line_2")]
        public string LocationLine2 { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(JsonIntToDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(JsonIntToDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }

        public String FeaturedDate
        {
            get { return $"{Month} {Day}"; }
        }

        public override string ToString()
        {
            return $"{ShortMonth} {Day}, {Label}";
        }
    }
}
