﻿//using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Commons.Ordering;
using POS.Application.Extensions.WatchDog;
using POS.Application.Interfaces;
using POS.Application.Services;
using System.Reflection;

namespace POS.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            //services.AddFluentValidation(options =>
            //{
            //    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()
            //        .Where(p => !p.IsDynamic));
            //});
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddTransient<IOrderingQuery, OrderingQuery>();

            services.AddScoped<IGenerateExcelApplication, GenerateExcelApplication>();

            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IProviderApplication, ProviderApplication>();

            services.AddScoped<IAuthApplication, AuthApplication>();
            
            services.AddScoped<IDocumentTypeApplication, DocumentTypeApplication>();
            services.AddScoped<IClientApplication, ClientApplication>();

            services.AddWathcDog(configuration);

            return services;
        }
    }
}
