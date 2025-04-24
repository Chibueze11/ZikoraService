using System.Numerics;
using System.Security.Principal;
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
    public class OrganizationService : IOrganizationService
    {
        private readonly IRemoteHttpClient _httpClient;
        private readonly ILogger<OrganizationService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<ExternalApiSettings> _apiSettings;
        private readonly ITokenService _tokenService;


        public OrganizationService(IRemoteHttpClient httpClient, IUnitOfWork unitOfWork,
            ZikoraRequestValidator validator,
            IOptions<ExternalApiSettings> apiSettings,
            ILogger<OrganizationService> logger,
            IMapper mapper, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<ExecutionResult<OrganizationResponse>> CreateOrganizationAsync(
                    OrganizationDto org)
                    
        {
            try
            {
              
                var payload = new
                {
                    accName = org.AccountName,
                    phone = org.Phone,
                    postalAddress = org.PostalAddress,
                    businessName = org.BusinessName,
                    taxIdNo = org.TaxIdNo,
                    tradeName = org.TradeName,
                    email = org.Email,
                    address = org.Address,
                    companyRegDate = org.CompanyRegDate,
                    businessCommencementDate = org.BusinessCommencementDate,
                    registrationNumber = org.RegistrationNumber,
                    customerMembers = org.CustomerMembers,
                    theDirectors = org.TheDirectors
                };

                // string requestUrl = "https://zikora-node.herokuapp.com/zikora-api/v1/auth/create-org-customer";

                string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-org-customer";

                _logger.LogInformation("Resolved Zikora URL: {Url}", requestUrl);
                _logger.LogInformation("Calling Zikora API at {Url}", requestUrl);


                // Match Postman's header format exactly
                var headers = new Dictionary<string, string>
                {
                   { "token", _tokenService.Token },  
                   { "refreshToken", _tokenService.RefreshToken },
                   { "Content-Type", "application/json" }
                };

                // Log exact headers being sent
                _logger.LogInformation("Request Headers: {@Headers}", headers);
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, payload, headers);

                var entity = _mapper.Map<Organization>(org);
                await _unitOfWork.Repository<Organization>().AddAsync(entity);
                await _unitOfWork.CompleteAsync();

                var customerId = response.GetProperty("customerId").GetString();
                var message = response.GetProperty("message").GetString();

                return new ExecutionResult<OrganizationResponse>
                {
                    Response = ResponseCode.Ok,
                    Result = new OrganizationResponse
                    {
                        customerId = customerId,
                        message = message
                    },
                    Message = message,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create Organization");
                return new ExecutionResult<OrganizationResponse>
                {
                    Response = ResponseCode.Failed,
                    Result = new OrganizationResponse
                    {
                        customerId = null,
                        message = $"{ex.Message} An error occurred while creating the Organization"
                    },
                };
            }
        }

    }

}
