using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cosmos.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "isCompleted")]
        public string Completed { get; set; }
    }
}