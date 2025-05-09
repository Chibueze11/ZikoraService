﻿using Microsoft.Extensions.DependencyInjection;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Services;

namespace ZikoraService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IVirtualAccountService, VirtualAccountService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ICorporateAccountService, CorporateAccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ZikoraRequestValidator>();


            return services;
        }
    }
}
