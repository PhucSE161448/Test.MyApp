﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Test.MyApp.Web.Configuration
{
    public static class Swagger
    {
        public static IServiceCollection AddSwaggerGenOption(this IServiceCollection service)
        {
            return service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyApp API",
                    Version = "v1",
                    Description = "MyApp API",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact Developers",
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
