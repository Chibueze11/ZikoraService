using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Services
{
    public class ZikoraRequestValidator
    {
        private readonly ILogger<ZikoraRequestValidator> _logger;

        public ZikoraRequestValidator(ILogger<ZikoraRequestValidator> logger)
        {
            _logger = logger;
        }

        public void ValidateCustomerRequest(CustomerDto request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                // Required fields validation
                if (string.IsNullOrWhiteSpace(request.FirstName))
                    throw new ArgumentException("First name (fName) is required");

                if (string.IsNullOrWhiteSpace(request.LastName))
                    throw new ArgumentException("Last name (lName) is required");

                if (string.IsNullOrWhiteSpace(request.BVN) || !Regex.IsMatch(request.BVN, @"^\d{11}$"))
                    throw new ArgumentException("BVN must be 11 digits");

                // Updated Nigerian phone number validation
                if (string.IsNullOrWhiteSpace(request.Phone) || !IsValidNigerianPhoneNumber(request.Phone))
                    throw new ArgumentException("Phone number must be a valid Nigerian number (e.g., 08012345678, 07011223344, 09098765432, 08123456789)");

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentException("Email is required");

                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new ArgumentException("Password is required");

                if (string.IsNullOrWhiteSpace(request.HomeAddress))
                    throw new ArgumentException("Home address is required");

                if (string.IsNullOrWhiteSpace(request.Gender))
                    throw new ArgumentException("Gender is required");

                if (string.IsNullOrWhiteSpace(request.ImageUrl))
                    throw new ArgumentException("Image URL is required");

                if (string.IsNullOrWhiteSpace(request.SignatureUrl))
                    throw new ArgumentException("Signature URL is required");

                if (string.IsNullOrWhiteSpace(request.IDImageUrl))
                    throw new ArgumentException("ID image URL is required");

                if (string.IsNullOrWhiteSpace(request.DocumentOne))
                    throw new ArgumentException("Document one is required");

                if (string.IsNullOrWhiteSpace(request.PlaceOfBirth))
                    throw new ArgumentException("Place of birth is required");

                if (string.IsNullOrWhiteSpace(request.AccountOfficerCode))
                    throw new ArgumentException("Account officer code is required");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request validation failed");
                throw;
            }
        }

        private bool IsValidNigerianPhoneNumber(string phoneNumber)
        {
            // Remove any whitespace or special characters
            var cleanedNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

            // Nigerian phone numbers:
            // - Start with 0
            // - Second digit is 7, 8, or 9
            // - Third digit is 0-9 (some providers use 1-9)
            // - Total 11 digits
            return Regex.IsMatch(cleanedNumber, @"^0[7-9][0-9]\d{8}$");
        }
    }
}