using System.Text.Json.Serialization;

namespace ZikoraService.Application.Dtos
{
    public class OrganizationDto
    {
        [JsonPropertyName("accName")]
        public string AccountName { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonPropertyName("businessName")]
        public string BusinessName { get; set; }

        [JsonPropertyName("tradeName")]
        public string TradeName { get; set; }

        [JsonPropertyName("taxIdNo")]
        public string TaxIdNo { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("companyRegDate")]
        public DateTime CompanyRegDate { get; set; }

        [JsonPropertyName("businessCommencementDate")]
        public DateTime BusinessCommencementDate { get; set; }

        [JsonPropertyName("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonPropertyName("customerMembers")]
        public List<string> CustomerMembers { get; set; }

        [JsonPropertyName("theDirectors")]
        public List<string> TheDirectors { get; set; }

    }

    public class OrganizationResponse
    {
        public string message { get; set; }
        public string customerId { get; set; }
    }
}
