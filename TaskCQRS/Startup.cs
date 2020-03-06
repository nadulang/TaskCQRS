using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskCQRS.Infrastructure.Persistences;
using TaskCQRS.Application.UseCases.Customer.Queries.GetCustomer;
using TaskCQRS.Application.UseCases.CustomerPayment.Queries.GetCustomerPayment;
using TaskCQRS.Application.Interfaces;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Application.UseCases.Customer.Command.CreateCustomer;
using System.Reflection;
using TaskCQRS.Application.UseCases.CustomerPayment.Command.CreateCustomerPayment;
using TaskCQRS.Application.UseCases.Merchant.Command.CreateMerchant;
using TaskCQRS.Application.UseCases.Product.Command.CreateProduct;

namespace TaskCQRS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EcommerceContext>(options => options.UseNpgsql("Host=127.0.0.1;Database=ecommercedb;Username=postgres;Password=docker"));
            services.AddControllers();
            services.AddMvc()
                   .AddFluentValidation();

            services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidation>();
            services.AddTransient<IValidator<CreateCustomerPaymentCommand>, CreateCustomerPaymentCommandValidation>();
            services.AddTransient<IValidator<CreateMerchantCommand>, CreateMerchantCommandValidaton>();
            services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidation>();
                
            services.AddMediatR(typeof(GetCustomerQueryHandler).GetTypeInfo().Assembly)
                    .AddMediatR(typeof(GetCustomerPaymentQueryHandler).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidatorBehaviour<,>));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.RequireHttpsMetadata = false;
                option.SaveToken = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secretnya kurang panjaaaaaangggggg banget")),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
