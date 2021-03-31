using System;
using System.Text;
using AppGmz.Core;
using AppGmz.DAL;
using AppGmz.DAL.Repository;
using AppGmz.Models.AppSettings;
using AppGmz.Models.IdentityModels;
using AppGmz.Services.PrepDb;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace AppGmzAPI
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
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddTransient<IRecordNewsRepository, RecordNewsRepository>();

            services.AddCors();
            services.AddControllers();

            //Setting CQRS 
            var assembly = AppDomain.CurrentDomain.Load("AppGmz.CQRS");
            services.AddMediatR(assembly);

            services.AddAutoMapper(typeof(AppGmz.Services.MapperService.AutoMapperApp).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppGmzAPI", Version = "v1" });
            });

            var server = Configuration["ServerDb"] ?? "localhost";
            var port = Configuration["PortDb"] ?? "1433";
            var catalog = Configuration["CatalogNameDb"] ?? "MyTestDb";
            var password = Configuration["Password"] ?? "P@55w0rd11";
            var user = Configuration["UserNameDb"] ?? "SA";

            //TODO: Change ConnectionString for DockerContainer!!!
            services.AddDbContext<AppDbContext>(op =>
            {
                //op.UseSqlServer($"Server={server},{port};Initial Catalog={catalog};User ID={user};Password={password}");
                op.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, AppRole>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>();
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            Console.WriteLine(key);
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(j =>
                {
                    j.RequireHttpsMetadata = false;
                    j.SaveToken = false;
                    j.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppGmzAPI v1"));
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            PrepDb.SetDate(app);
            app.UseRouting();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}