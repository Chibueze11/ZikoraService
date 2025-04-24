using System.Text.Json.Serialization;

namespace ZikoraService.Application.Dtos
{
    public class CorporateAccountDto
    {
        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("accountOpeningTrackingRef")]
        public string AccountOpeningTrackingRef { get; set; }

        [JsonPropertyName("productCode")]
        public string ProductCode { get; set; }

        [JsonPropertyName("accountName")]
        public string AccountName { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("accountOfficerCode")]
        public string AccountOfficerCode { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("transactionPermission")]
        public string TransactionPermission { get; set; }

        [JsonPropertyName("notificationPreference")]
        public int NotificationPreference { get; set; }

        [JsonPropertyName("accountTier")]
        public string AccountTier { get; set; }

       
    }
    public class CorporateResponse
    {
        public string message { get; set; }
        public string customerId { get; set; }
    }
}
