using Certes;
using FluffySpoon.AspNet.EncryptWeMust;
using FluffySpoon.AspNet.EncryptWeMust.Certes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using reg.Extensions.IOC;
using Scaffolds;
using System;
using System.Text;

namespace reg
{
    public class Startup
    {
        public string DomainToUse = "regcodenation.ddns.net";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //the following line adds the automatic renewal service.
            services.AddFluffySpoonLetsEncrypt(new LetsEncryptOptions()
            {                
                Email = "contato.danielharo@gmail.com", //LetsEncrypt will send you an e-mail here when the certificate is about to expire
                UseStaging = false, //switch to true for testing
                Domains = new[] { DomainToUse },
                TimeUntilExpiryBeforeRenewal = TimeSpan.FromDays(30), //renew automatically 30 days before expiry
                TimeAfterIssueDateBeforeRenewal = TimeSpan.FromDays(7), //renew automatically 7 days after the last certificate was issued
                CertificateSigningRequest = new CsrInfo() //these are your certificate details
                {
                    CountryName = "Brazil",
                    Locality = "SP",
                    Organization = "Student",
                    OrganizationUnit = "Student",
                    State = "SP"
                }
            });

            //the following line tells the library to persist the certificate to a file, so that if the server restarts, the certificate can be re-used without generating a new one.
            services.AddFluffySpoonLetsEncryptFileCertificatePersistence();

            //the following line tells the library to persist challenges in-memory. challenges are the "/.well-known" URL codes that LetsEncrypt will call.
            services.AddFluffySpoonLetsEncryptMemoryChallengePersistence();

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Central de Erros",
                        Version = "v1",
                        Description = "API REST criada com o ASP.NET Core 3.1 para criação e consulta de logs",
                        Contact = new OpenApiContact
                        {
                            Name = "Daniel Haro",
                            Url = new Uri("https://github.com/danielmorine")
                        }
                    });
            });

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.RepositoryIOC().ServiceIOC();

            services.AddMvc();
            services.AddHttpContextAccessor(); ;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
       
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.
                    ASCII.GetBytes(Configuration["TokenConfiguration:Secret"])),
                    ValidAudience = Configuration["TokenConfiguration:Audience"],
                    ValidIssuer = Configuration["TokenConfiguration:Issuer"]
                };                
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
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
