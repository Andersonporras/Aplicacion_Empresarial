using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Pacagroup.Ecommerce.Transversal.Mapper;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Main;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using System.ComponentModel;


namespace Pacagroup.Ecommerce.Services.WebApi
{
    public class Startup
    {
        readonly string myPolicy = "policyApiEcommerce";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
            services.AddCors(options => options.AddPolicy(myPolicy, builder => builder.WithOrigins(Configuration["Config:OriginCors"])
                                                                                       .AllowAnyHeader()
                                                                                       .AllowAnyMethod()));
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //pendiente para verificar video 17 28:21
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(options =>{ options.SerializerSettings.ContractResolver = new DefaultContractResolver();});

            services.AddSingleton<IConfiguration>(Configuration);           
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        version = "v1",
            //        title = "Pacagroup Technology Services API Market",
            //        description = "A simple example ASP.NET Core Web API",
            //        termsOfService = "None",
            //        contact = new Contact
            //        {
            //            name = "Alex Espejo",
            //            email = "alex.espejo.c@gmail.com",
            //            url = "https://pacagroup.com"
            //        },
            //        license = new Swashbuckle.Swagger.License
            //        {
            //            name = "Use under LICX",
            //            url = "https://example.com/license"
            //        }
            //    });
            //    // Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce V1");
            });

            app.UseCors(myPolicy);
            app.UseMvc();

        }
    }
}

//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Pacagroup.Ecommerce.Transversal.Mapper;
//using Pacagroup.Ecommerce.Transversal.Common;
//using Pacagroup.Ecommerce.Infrastructure.Repository;
//using Pacagroup.Ecommerce.Infrastructure.Interface;
//using Pacagroup.Ecommerce.Domain.Interface;
//using Pacagroup.Ecommerce.Domain.Core;
//using Pacagroup.Ecommerce.Application.Interface;
//using Pacagroup.Ecommerce.Application.Main;
//using Pacagroup.Ecommerce.Infrastructure.Data;

//namespace Pacagroup.Ecommerce.Service.WebApii
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {

//            services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pacagroup.Ecommerce.Service.WebApii", Version = "v1" })
//                ;
//            });
//            //solo se especifica la vida util del servicio por q la inyeccion de dependencias se da de manera nativa
//            services.AddSingleton<IConfiguration>(Configuration);
//            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
//            services.AddScoped<ICustomersApplication, CustomersApplication>();
//            services.AddScoped<ICustomersDomain, CustomersDomain>();
//            services.AddScoped<ICustomersRepository, CustomersRepository>();
//            //pendiente para verificar video 17 28:21
//            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
//            //    .AddJsonOptions(options => { options. = new });
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseSwagger();
//                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pacagroup.Ecommerce.Service.WebApii v1"));
//            }

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
