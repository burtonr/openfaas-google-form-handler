using Newtonsoft.Json;

namespace Function
{
    public class FormResponse
    {
        [JsonProperty("Email")]
        public string Email { get; set; }
        
        [JsonProperty("First Name")]
        public string FirstName { get; set; }
        
        [JsonProperty("Last Name")]
        public string LastName { get; set; }
        
        [JsonProperty("Company")]
        public string Company { get; set; }
        
        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("I'm joining to")]
        public string JoinReason { get; set; }
    }
}