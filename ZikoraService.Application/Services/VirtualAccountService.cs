using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Domain.Entities;
using ZikoraService.Infrastructure.Http;
using ZikoraService.Infrastructure.Persistence.UnitOfWork;

namespace ZikoraService.Application.Services
{
    public class VirtualAccountService : IVirtualAccountService
    {
        private readonly IRemoteHttpClient _httpClient;
        private readonly ILogger<VirtualAccountService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<ExternalApiSettings> _apiSettings;

        public VirtualAccountService(IRemoteHttpClient httpClient, IOptions<ExternalApiSettings> apiSettings, ILogger<VirtualAccountService> logger,
            IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

       
        public async Task<ExecutionResult<dynamic>> CreateVirtualAccountAsync(CreateVirtualAccountDto dto)
        {
            dto.TrackingRef = Guid.NewGuid().ToString();
            var requestPayload = new
            {
                accountNumber = dto.AccountNumber,
                productCode = dto.ProductCode,
                noOfAccounts = dto.NoOfAccounts,
                trackingRef = dto.TrackingRef,
            };

            // string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/create-virtual-account";
            string requestUrl = "https://zikora-node.herokuapp.com/zikora-api/v1/auth/create-virtual-account";

            try
            {
                var headers = new Dictionary<string, string>
                {
                   { "token", _tokenService.Token },
                   { "refreshToken", _tokenService.RefreshToken },
                   { "Content-Type", "application/json" }
                };

                // Log exact headers being sent
                _logger.LogInformation("Request Headers: {@Headers}", headers);
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload, headers);

                var virtualAccount = new VirtualAccount
                {
                    AccountNumber = dto.AccountNumber,
                    ProductCode = dto.ProductCode,
                    NumberOfAccounts = dto.NoOfAccounts,
                    TrackingReference = dto.TrackingRef,
                };

                var entity = _mapper.Map<VirtualAccount>(dto);
                await _unitOfWork.Repository<VirtualAccount>().AddAsync(entity);
                await _unitOfWork.CompleteAsync();

                return new ExecutionResult<dynamic>
                {
                    Response = ResponseCode.Ok,
                    Result = response,
                    Message = "Virtual account(s) created successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating virtual account(s).");
                return new ExecutionResult<dynamic>
                {
                    Response = ResponseCode.Failed,
                    Message = ex.Message
                };
            }
        }

        public async Task<ExecutionResult<dynamic>> InitiatePaymentAsync(InitiatePaymentDto dto)
        {
            var requestPayload = new
            {
                accountNumber = dto.AccountNumber,
                amount = dto.Amount,
            };

            // string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/initiate-virtual-payment";
            string requestUrl = "https://zikora-node.herokuapp.com/zikora-api/v1/auth/initate-virtualpayment";
            try
            {
                var headers = new Dictionary<string, string>
                {
                   { "token", _tokenService.Token },
                   { "refreshToken", _tokenService.RefreshToken },
                   { "Content-Type", "application/json" }
                };

                // Log exact headers being sent
                _logger.LogInformation("Request Headers: {@Headers}", headers);
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload, headers);

                var virtualAccount = new VirtualAccountTransaction
                {
                    AccountNumber = dto.AccountNumber,
                   Amount = dto.Amount,
                };

               
                var entity = _mapper.Map<VirtualAccountTransaction>(dto);
                await _unitOfWork.Repository<VirtualAccountTransaction>().AddAsync(entity);
                await _unitOfWork.CompleteAsync();


                return new ExecutionResult<dynamic>
                {
                    Response = ResponseCode.Ok,
                    Result = response,
                    Message = "Payment initiated successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating payment.");
                return new ExecutionResult<dynamic>
                {
                    Response = ResponseCode.Failed,
                    Message = ex.Message
                };
            }
        }

        public async Task<ExecutionResult<dynamic>> VerifyTransactionAsync(VerifyTransactionDto dto)
        {
            var requestPayload = new
            {
                uniqueReference = dto.UniqueReference,
            };

            // string requestUrl = $"{_apiSettings.Value.BaseUrl}/auth/verify-virtual-trx";
            string requestUrl = "https://zikora-node.herokuapp.com/zikora-api/v1/auth/verify-virtual-trx";
            try
            {
                var headers = new Dictionary<string, string>
                {
                   { "token", _tokenService.Token },
                   { "refreshToken", _tokenService.RefreshToken },
                   { "Content-Type", "application/json" }
                };

                // Log exact headers being sent
                _logger.LogInformation("Request Headers: {@Headers}", headers);
                var response = await _httpClient.PostJSON<dynamic>(requestUrl, requestPayload, headers);


                var virtualAccount = new VirtualAccountTransaction
                {
                   UniqueReference = dto.UniqueReference,
                    IsVerified = response.IsVerified,
                };


                var entity = _mapper.Map<VirtualAccountTransaction>(dto);
                await _unitOfWork.Repository<VirtualAccountTransaction>().AddAsync(entity);
                await _unitOfWork.CompleteAsync();


                return new ExecutionResult<dynamic>
                {
                    Response = ResponseCode.Ok,
                    Result = response,
                    Message = "Transaction verification successful."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying transaction.");
                return new ExecutionResult<dynamic>
                {
                    Response = ResponseCode.Failed,
                    Message = ex.Message
                };
            }
        }
    }

}
