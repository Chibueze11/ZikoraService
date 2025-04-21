using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Infrastructure.Http;
using ZikoraService.Infrastructure.Persistence.UnitOfWork;

namespace ZikoraService.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRemoteHttpClient _httpClient;
        private readonly ZikoraRequestValidator _validator;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<ExternalApiSettings> _apiSettings;

        public CustomerService(
            IRemoteHttpClient httpClient,
            IUnitOfWork unitOfWork,
            ZikoraRequestValidator validator,
            IOptions<ExternalApiSettings> apiSettings,
            ILogger<CustomerService> logger,
            IMapper mapper)
        {
            _httpClient = httpClient;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _apiSettings = apiSettings;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CustomerDto customerDto)
        {
            try
            {
                // Validate request
                _validator.ValidateCustomerRequest(customerDto);

                // formatted request payload
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
                    dob = customerDto.DateOfBirth,
                    docOne = customerDto.DocumentOne,
                    referallId = customerDto.ReferralId,
                    placeOfBirth = customerDto.PlaceOfBirth,
                    nokName = customerDto.NextOfKinName,
                    nokPhone = customerDto.NextOfKinPhone,
                    accountOfficerCode = customerDto.AccountOfficerCode
                };

                string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-customer";
                _logger.LogInformation("Resolved Zikora URL: {Url}", requestUrl);
                _logger.LogInformation("Calling Zikora API at {Url} with Payload: {Payload}", requestUrl, JsonConvert.SerializeObject(requestPayload));
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload);

              _unitOfWork.Repository<CustomerDto>().AddAsync(response);
                return new CustomerResponse
                {
                    customerId = response.CustomerId,
                    message = response.Message,
                };

                // Use PostJSON with dynamic type to handle the response
              //dynamic response = await _httpClient.PostJSON<dynamic>("auth/create-customer", requestPayload);
                // Return the customerId from the response
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create customer");
                return new CustomerResponse
                {
                    message = $"{ex.Message}\n  An error occurred while creating the customer"
                };
            }
        }
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var payload = new
            {
                email = loginDto.Email,
                password = loginDto.Password,
                device_id = loginDto.DeviceId,
                device_type = loginDto.DeviceType,
                device_name = loginDto.DeviceName
            };

            string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-customer";
            _logger.LogInformation("Resolved Zikora URL: {Url}", requestUrl);
            _logger.LogInformation("Calling Zikora API at {Url} with payload: {payload}", requestUrl, JsonConvert.SerializeObject(payload));
            var response = await _httpClient.PostJSON<dynamic>(requestUrl, payload);
            //ar response = await _httpClient.PostAsync("authenticate", payload);
            return null;
        }
    }
}