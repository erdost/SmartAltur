using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartAltur.DTO
{
    // With the help of http://json2csharp.com/
    public class DistanceResponse
    {
        [JsonProperty(PropertyName = "destination_addresses")]
        public List<string> DestinationAddresses { get; set; }

        [JsonProperty(PropertyName = "origin_addresses")]
        public List<string> OriginAddresses { get; set; }

        public List<Row> Rows { get; set; }

        public string Status { get; set; }

        public class Distance
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        public class Duration
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        public class Element
        {
            public Distance Distance { get; set; }
            public Duration Duration { get; set; }
            public string Status { get; set; }
        }

        public class Row
        {
            public List<Element> Elements { get; set; }
        }
    }
}
