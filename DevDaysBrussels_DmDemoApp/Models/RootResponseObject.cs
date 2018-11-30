using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.Models
{
    public class RootResponseObject<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int page { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int limit { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool @explicit { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int total { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool has_more { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<T> list { get; set; }
    }


    public class VideoItems
    {
        [JsonProperty("channel.name", NullValueHandling = NullValueHandling.Ignore)]
        public string channelName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumbnail_url { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title { get; set; }

        [JsonProperty("owner.username", NullValueHandling = NullValueHandling.Ignore)]
        public string  username { get; set; }
    }
}
