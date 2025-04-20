namespace ZikoraService.Domain.Entities
{
    public class CorporateAccount
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string AccountOpeningTrackingRef { get; set; }
        public string ProductCode { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string AccountOfficerCode { get; set; }
        public string Email { get; set; }
        public string TransactionPermission { get; set; }
        public int NotificationPreference { get; set; }
        public string AccountTier { get; set; }
    }
}
