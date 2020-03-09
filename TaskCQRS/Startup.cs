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
using TaskCQRS.Application.UseCases.Product.Queries.GetProduct;
using TaskCQRS.Application.UseCases.Merchant.Queries.GetMerchant;
using TaskCQRS.Application.Interfaces;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Application.UseCases.Customer.Command.CreateCustomer;
using TaskCQRS.Application.UseCases.Customer.Command.UpdateCustomer;
using System.Reflection;
using TaskCQRS.Application.UseCases.CustomerPayment.Command.CreateCustomerPayment;
using TaskCQRS.Application.UseCases.CustomerPayment.Command.UpdateCustomerPayment;
using TaskCQRS.Application.UseCases.Merchant.Command.CreateMerchant;
using TaskCQRS.Application.UseCases.Merchant.Command.UpdateMerchant;
using TaskCQRS.Application.UseCases.Product.Command.CreateProduct;
using TaskCQRS.Application.UseCases.Product.Command.UpdateProduct;
using Hangfire;
using Hangfire.PostgreSql;

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
            services.AddHangfire(config =>
                        config.UsePostgreSqlStorage("Host=127.0.0.1;Database=cobabackgrounddb;Username=postgres;Password=docker"));
            services.AddMvc()
                   .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining(typeof(CreateProductCommandValidation)));

            services.AddMediatR(typeof(GetCustomerQueryHandler).GetTypeInfo().Assembly);
       
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

            app.UseHangfireServer();

            app.UseHangfireDashboard();
            BackgroundJob.Enqueue(() => Console.WriteLine("Coba Hangfire."));
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring Task"), Cron.Minutely);

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
