namespace ZikoraService.Domain.Entities
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string Phone { get; set; }
        public string PostalAddress { get; set; }
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public string TaxIdNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CompanyRegDate { get; set; }
        public DateTime BusinessCommencementDate { get; set; }
        public string RegistrationNumber { get; set; }
        public List<string> CustomerMembers { get; set; }
        public List<string> TheDirectors { get; set; }
    }
}
