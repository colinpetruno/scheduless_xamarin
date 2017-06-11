using System;
using Newtonsoft.Json;
using Scheduleless.Models.Converters;

namespace Scheduleless.Models
{


    public class Offer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("location_city_state_zip")]
        public string LocationCityStateZip { get; set; }

        [JsonProperty("location_name")]
        public string LocationName { get; set; }

        [JsonProperty("location_line_1")]
        public string LocationLine1 { get; set; }

        [JsonProperty("location_line_2")]
        public string LocationLine2 { get; set; }

        [JsonProperty("location_postalcode")]
        public string LocationPostalcode { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("offered_by_name")]
        public string OfferedByName { get; set; }

        [JsonProperty("shift_end_time")]
        public string ShiftEndTime { get; set; }

        [JsonProperty("shift_date")]
        public string ShiftDate { get; set; }

        [JsonProperty("shift_label")]
        public string ShiftLabel { get; set; }

        [JsonProperty("shift_short_month")]
        public string ShiftShortMonth { get; set; }

        [JsonProperty("shift_start_time")]
        public string ShiftStartTime { get; set; }
    }
}
