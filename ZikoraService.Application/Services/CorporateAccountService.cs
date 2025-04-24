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
    public class CorporateAccountService : ICorporateAccountService
    {
        private readonly IRemoteHttpClient _httpClient;
        private readonly ILogger<CorporateAccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<ExternalApiSettings> _apiSettings;
        private readonly ITokenService _tokenService;


        public CorporateAccountService(IRemoteHttpClient httpClient, IUnitOfWork unitOfWork,
            ZikoraRequestValidator validator,
            IOptions<ExternalApiSettings> apiSettings,
            ILogger<CorporateAccountService> logger,
            IMapper mapper, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<ExecutionResult<CorporateResponse>> CreateCorporateAccountAsync(CorporateAccountDto account)
        {
            try
            {
                var requestPayload = new
                {
                    customerId = account.CustomerId,
                    accountOpeningTrackingRef = account.AccountOpeningTrackingRef,
                    productCode = account.ProductCode,
                    accountName = account.AccountName,
                    phoneNumber = account.PhoneNumber,
                    gender = account.Gender,
                    address = account.Address,
                    accountOfficerCode = account.AccountOfficerCode,
                    email = account.Email,
                    transactionPermission = account.TransactionPermission,
                    notificationPreference = account.NotificationPreference,
                    accountTier = account.AccountTier
                };

               // string requestUrl = "https://zikora-node.herokuapp.com/zikora-api/v1/auth/create-corporate-account";

                var headers = new Dictionary<string, string>
                {
                      { "token", _tokenService.Token },  
                      { "refreshToken", _tokenService.RefreshToken }, 
                      { "Content-Type", "application/json" }
                };

                // Log the exact headers being sent
                _logger.LogInformation("Request Headers: {@Headers}", headers);

                string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-corporate-account";
                _logger.LogInformation("Resolved Zikora URL: {Url}", requestUrl);
                _logger.LogInformation("Calling Zikora API at {Url} with Payload: {Payload}", requestUrl, JsonConvert.SerializeObject(requestPayload));
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload,headers);

                var entity = _mapper.Map<CorporateAccount>(account);

                await _unitOfWork.Repository<CorporateAccount>().AddAsync(entity);
                await _unitOfWork.CompleteAsync();


                var customerId = response.GetProperty("customerId").GetString();
                var message = response.GetProperty("message").GetString();

                return new ExecutionResult<CorporateResponse>
                {
                    Response = ResponseCode.Ok,
                    Result = new CorporateResponse
                    {
                        customerId = customerId,
                        message = message
                    },
                    Message = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create Corporate Account");
                return new ExecutionResult<CorporateResponse>
                {
                    Response = ResponseCode.Failed,
                    Result = new CorporateResponse
                    {
                        customerId = null,
                        message = $"{ex.Message} An error occurred while creating the Corporate Account"
                    },
                    Message= ex.Message
                };

            }

        }
    }
}
