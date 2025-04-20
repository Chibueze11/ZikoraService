namespace ZikoraService.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string BVN { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string HomeAddress { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Signature { get; set; }
        public string IDImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        public string IdNumber { get; set; }
        public string AccountOfficerCode { get; set; }
        public string DocOne { get; set; }
        public string ReferralId { get; set; }
        public string NOKName { get; set; }
        public string NOKPhone { get; set; }
    }
}
