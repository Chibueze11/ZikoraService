using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Domain.Entities;
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

                string requestUrl= "https://zikora-node.herokuapp.com/zikora-api/v1/auth/create-customer";
               // string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-customer";
                _logger.LogInformation("Resolved Zikora URL: {Url}", requestUrl);
                _logger.LogInformation("Calling Zikora API at {Url} with Payload: {Payload}", requestUrl, JsonConvert.SerializeObject(requestPayload));
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload);

                var customerEntity = _mapper.Map<Customer>(customerDto);

                await _unitOfWork.Repository<Customer>().AddAsync(customerEntity);
                await _unitOfWork.CompleteAsync();


                var customerId = response.GetProperty("customerId").GetString(); 
                var message = response.GetProperty("message").GetString();

                return new CustomerResponse
                {
                    customerId = customerId,
                    message = message,
                };

               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create customer");
                return new CustomerResponse
                {
                    message = $"{ex.Message} An error occurred while creating the customer"
                };
            }
        }
        public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
        {
            var payload = new
            {
                email = loginDto.Email,
                password = loginDto.Password,
                device_id = loginDto.DeviceId,
                device_type = loginDto.DeviceType,
                device_name = loginDto.DeviceName
            };

            string requestUrl = "https://zikora-node.herokuapp.com/zikora-api/v1/authenticate";
            _logger.LogInformation("Resolved Zikora URL: {Url}", requestUrl);
            _logger.LogInformation("Calling Zikora API at {Url} with payload: {Payload}", requestUrl, JsonConvert.SerializeObject(payload));

            try
            {
                var response = await _httpClient.PostJSON<AuthResponse>(requestUrl, payload);

                if (response == null || string.IsNullOrEmpty(response.Token))
                {
                    _logger.LogWarning("Login failed or token missing.");
                    return null;
                }

                _logger.LogInformation("Login successful. User ID: {UserId}, Token: {Token}", response.Auth?.Id, response.Token);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calling the login API.");
                throw;
            }
        }



    }
}