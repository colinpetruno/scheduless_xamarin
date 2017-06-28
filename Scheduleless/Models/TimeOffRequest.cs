using System;
using Newtonsoft.Json;
using Scheduleless.Models.Converters;

namespace Scheduleless.Models
{

  public class TimeOffRequest
  {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("background_color")]
    public string BackgroundColor { get; set; }

    [JsonProperty("border_color")]
    public string BorderColor { get; set; }

    [JsonProperty("text_color")]
    public string TextColor { get; set; }

  }
}
