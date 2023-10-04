using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "Pos Api",
                Version = "v1",
                Description = "Punto Venta 2023",
                TermsOfService = new Uri("http://opensource.org/licenses/NIT"),
                Contact = new OpenApiContact
                {
                    Name = "rrcz s.a.",
                    Email = "robertoucbnh@gmail.com",
                    Url = new Uri("https://rsoliz.com.bo/"),
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LINK",
                    Url = new Uri("http://opensource.org/licenses/")
                }
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentification",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[]{} }
                });
            });

            return services;


        }
    }
}
