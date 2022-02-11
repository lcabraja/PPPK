using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonManager.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Age")]
        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}