using System;
using System.Text.Json.Serialization;

namespace ZikoraService.Application.Dtos
{
    public class LoginDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

    }
    public class CustomerDto
    {
        // Match exact property names from API documentation
        [JsonPropertyName("fName")]
        public string FirstName { get; set; }  // Changed from FName to FirstName for better naming

        [JsonPropertyName("mName")]
        public string? MiddleName { get; set; }  // Changed from MName to MiddleName

        [JsonPropertyName("lName")]
        public string LastName { get; set; }  // Changed from LName to LastName

        [JsonPropertyName("bvn")]
        public string BVN { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("idNum")]
        public string? IdNumber { get; set; }

        [JsonPropertyName("pin")]
        public string? PIN { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("homeAddress")]
        public string HomeAddress { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("signature")]
        public string SignatureUrl { get; set; } 

        [JsonPropertyName("idImage")]
        public string IDImageUrl { get; set; }

        [JsonPropertyName("dob")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("docOne")]
        public string DocumentOne { get; set; }

        [JsonPropertyName("referallId")]
        public string? ReferralId { get; set; }

        [JsonPropertyName("placeOfBirth")]
        public string PlaceOfBirth { get; set; }

        [JsonPropertyName("nokName")]
        public string? NextOfKinName { get; set; } 

        [JsonPropertyName("nokPhone")]
        public string? NextOfKinPhone { get; set; }

        [JsonPropertyName("accountOfficerCode")]
        public string AccountOfficerCode { get; set; }
    }
}