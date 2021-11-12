using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities.Identity;
using WeCare.Persistance;
using WeCare.Service;
using WeCare.Service.Impl;

namespace WeCare
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
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers();
            //services.AddDbContext<ApplicationDbContext>(
            //    opts => opts.UseSqlServer(Configuration.GetConnectionString("BrandonConnection"))
            //    );
            services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseNpgsql(Configuration.GetConnectionString("HerokuPostgre")));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTransient<SpecialistService, SpecialistServiceImpl>();
            services.AddTransient<PatientService, PatientServiceImpl>();
            services.AddTransient<EventService, EventServiceImpl>();
            AddSwagger(services);
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(opts =>
            {
                var groupName = "v1";
                opts.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"WeCare {groupName}",
                    Version = groupName,
                    Description = "WeCare API",
                    Contact = new OpenApiContact
                    {
                        Name = "WeCare Company",
                        Email = string.Empty,
                        Url = new Uri("https://wecare.com/")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeCare API V1");
            });

            app.UseRouting();
            app.UseCors("Cors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
