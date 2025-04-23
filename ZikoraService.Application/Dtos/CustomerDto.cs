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

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("device_type")]
        public string DeviceType { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

    }
    public class CustomerDto
    {
        // Match exact property names from API documentation
        [JsonPropertyName("fName")]
        public string FirstName { get; set; }

        [JsonPropertyName("mName")]
        public string? MiddleName { get; set; }

        [JsonPropertyName("lName")]
        public string LastName { get; set; }

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
        public DateOnly DateOfBirth { get; set; }

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

    public class CustomerResponse
    {
        public string message { get; set; }
        public string customerId { get; set; }
    }

    public class AuthResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public AuthUser Auth { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class AuthUser
    {
        public int Id { get; set; }
        public string F_Name { get; set; }
        public string M_Name { get; set; }
        public string L_Name { get; set; }
        public string Bvn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Id_Num { get; set; }
        public string Pin { get; set; }
        public int Confirmed { get; set; }
        public int Ranking { get; set; }
        public int Rankings { get; set; }
        public string Home_Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Signature { get; set; }
        public string Id_Image { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_At { get; set; }
        public string Customer_Bankone_Id { get; set; }
        public DateTime Dob { get; set; }
        public int User_Level { get; set; }
        public int Approved { get; set; }
        public string Reason { get; set; }
        public string Admin_Id { get; set; }
        public string Doc_One { get; set; }
        public string Doc_Verifications { get; set; }
        public int Pending_Update { get; set; }
        public string Referral_Id { get; set; }
        public string Notification_Channel { get; set; }
        public int Restrict_User { get; set; }
        public int Is_Synced { get; set; }
        public string Referral_Code { get; set; }
        public int Referral_Generated { get; set; }
        public DateTime Last_Reminded_Date { get; set; }
        public int Remind_To_Fund_Account { get; set; }
        public int Is_Claimed_Referral { get; set; }
        public int Bonus_Redeemed { get; set; }
    }

}


