using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ZikoraService.Domain.Entities
{
    public class Customer
    {
        [Key]
        public Guid MyCustomerKey { get; set; }

        public string FirstName { get; set; }

            public string? MiddleName { get; set; }

            public string LastName { get; set; }

            public string BVN { get; set; }

            public string Phone { get; set; }

            public string Email { get; set; }

            public string? IdNumber { get; set; }

            public string? PIN { get; set; }

            public string Password { get; set; }

            public string HomeAddress { get; set; }

            public string? City { get; set; }

            public string? State { get; set; }

            public string Gender { get; set; }

            public string ImageUrl { get; set; }

            public string SignatureUrl { get; set; }

            public string IDImageUrl { get; set; }

            public DateOnly DateOfBirth { get; set; }

            public string DocumentOne { get; set; }

            public string? ReferralId { get; set; }

            public string PlaceOfBirth { get; set; }

            public string? NextOfKinName { get; set; }

            public string? NextOfKinPhone { get; set; }

            public string AccountOfficerCode { get; set; }
        }

    
}
