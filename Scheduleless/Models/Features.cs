using System;
using Newtonsoft.Json;
using Scheduleless.Models.Converters;

namespace Scheduleless.Models
{
  public class Features
  {
    [JsonProperty("time_clock")]
    public Boolean TimeClock { get; set; }

    [JsonProperty("trading")]
    public Boolean Trading { get; set; }

    [JsonProperty("wages")]
    public Boolean Wages { get; set; }
  }
}
