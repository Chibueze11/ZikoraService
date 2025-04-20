using System.Net.Http;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Infrastructure.Http;
using static ZikoraService.Infrastructure.Http.RemoteHttpClient;

namespace ZikoraService.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRemoteHttpClient _httpClient;
        private readonly ZikoraRequestValidator _validator;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;
        private readonly IOptions<ExternalApiSettings> _apiSettings;

        public CustomerService(
            IRemoteHttpClient httpClient,
            ZikoraRequestValidator validator,
IOptions<ExternalApiSettings> apiSettings,
            ILogger<CustomerService> logger,
            IMapper mapper)
        {
            _httpClient = httpClient;
            _validator = validator;
            _apiSettings = apiSettings;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<string> CreateCustomerAsync(CustomerDto customerDto)
        {
            try
            {
                // Validate request before sending
                _validator.ValidateCustomerRequest(customerDto);

                // Create properly formatted request payload
                var requestPayload = new
                {
                    fName = customerDto.FirstName,
                    mName = customerDto.MiddleName,
                    lName = customerDto.LastName,
                    bvn = customerDto.BVN,
                    phone = customerDto.Phone,
                    email = customerDto.Email,
                    idNum = customerDto.IdNumber,
                    pin = customerDto.PIN,
                    password = customerDto.Password,
                    homeAddress = customerDto.HomeAddress,
                    city = customerDto.City,
                    state = customerDto.State,
                    gender = customerDto.Gender,
                    image = customerDto.ImageUrl,
                    signature = customerDto.SignatureUrl,
                    idImage = customerDto.IDImageUrl,
                    dob = customerDto.DateOfBirth.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    docOne = customerDto.DocumentOne,
                    referallId = customerDto.ReferralId,
                    placeOfBirth = customerDto.PlaceOfBirth,
                    nokName = customerDto.NextOfKinName,
                    nokPhone = customerDto.NextOfKinPhone,
                    accountOfficerCode = customerDto.AccountOfficerCode
                };

                string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-customer";
                _logger.LogInformation("Resolved OnePipe URL: {Url}", requestUrl);
                _logger.LogInformation("Calling OnePipe API at {Url} with Payload: {Payload}", requestUrl, JsonConvert.SerializeObject(requestPayload));
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload);
          //    _logger.LogInformation("Raw Response from OnePipe: {Response}", JsonConvert.SerializeObject(onePipeResponse));


                // Use PostJSON with dynamic type to handle the response
              //dynamic response = await _httpClient.PostJSON<dynamic>("auth/create-customer", requestPayload);

                // Return the customerId from the response
                return response.customerId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create customer");
                throw;
            }
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            var payload = new
            {
                email,
                password,
                device_id = "DEVICE-001",
                device_type = "web",
                device_name = "ZikoraApp"
            };

            //ar response = await _httpClient.PostAsync("authenticate", payload);
            return null;
        }
    }
}