using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;


namespace App.IocConfig.Api.Swagger
{
    public static class SwaggerConfigurationExtentions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = "Library Api",
                        Version = "v1",
                        Description = "Through this Api you can access .....",
                        Contact = new OpenApiContact
                        {
                            Email = "sadeqmozaffari@gmail.com",
                            Name = "sadeq mozaffari",
                            Url=  new Uri("http://www.sadeqmozaffari.com"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "License",
                            Url = new Uri("http://www.sadeqmozaffari.com"),
                        },
                    });
                //c.SwaggerDoc(
                //   "v2",
                //   new OpenApiInfo()
                //   {
                //       Title = "Library Api",
                //       Version = "v2",
                //       Description = "Through this Api you can access BookInfo",
                //       Contact = new OpenApiContact
                //       {
                //           Email = "sadeqmozaffari@gmail.com",
                //           Name = "sadeq mozaffari",
                //           Url = new Uri("http://www.sadeqmozaffari.com"),
                //       },
                //       License = new OpenApiLicense
                //       {
                //           Name = "License",
                //           Url = new Uri("http://www.sadeqmozaffari.com"),
                //       },
                //   });

                c.DescribeAllParametersInCamelCase();

                c.OperationFilter<RemoveVersionParameters>();
                c.DocumentFilter<SetVersionInPaths>();

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "Bearer");
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", new string[] { }}
                //});



                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerAndUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {   
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My Api v2");
            });

        }
    }
}
