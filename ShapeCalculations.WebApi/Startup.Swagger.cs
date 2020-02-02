using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ShapeCalculations.WebApi
{
    public partial class Startup
    {
        public void AddSwagger(IServiceCollection services)
        {
            static OpenApiInfo CreateInfo()
            {
                var info = new OpenApiInfo()
                {
                    Title = "ShapeCalclations API",
                    // Version = "v1.0",
                    Description = "API with .Core 3.1, Swagger and Swashbuckle to perform shape calculation in a 3D space.",
                    Contact = new OpenApiContact() { Name = "Marc Compta", Email = "marc.compta@sunwebgroup.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                };

                return info;
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", CreateInfo());
                c.EnableAnnotations();
                c.IgnoreObsoleteProperties();
                c.IgnoreObsoleteActions();
                c.DescribeAllParametersInCamelCase();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
