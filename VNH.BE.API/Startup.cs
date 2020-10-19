using Autofac;
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
using System.Text;
using VNH.BE.API.Infrastructure;
using VNH.BE.API.Infrastructure.Autofac;
using VNH.BE.API.Infrastructure.Models;
using VNH.BE.Domain.Aggregates.Identity;
using VNH.BE.Infrastructure;

namespace VNH.BE.API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            _ = services.AddMvc(options => {
                // For use enale default mvc routing
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddControllersAsServices();

            services.AddDbContext<ApplicationDbContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(config => {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"])),
                    ValidIssuer = Configuration["AppSettings:Issuer"],
                    ValidAudience = Configuration["AppSettings:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder => {
                                      builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddCustomeSwagger();

            //DI Register
            services.Configure<AuthenicateModel>(Configuration.GetSection("AppSettings"));
            services.AddOptions();

        }

        public void ConfigureContainer(ContainerBuilder builder) {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new ApplicationModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            // Endpoint for API controller
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            // Use default MVC controller Home position after Empoint config
            app.UseMvcWithDefaultRoute();

            DataSeeder.SeedData(app);
        }


    }

    static class CustomeExtensionConfigure {
        public static IServiceCollection AddCustomeSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "DDLS API", Version = "v1", Description = "The DDLS HTTP API", });

                var securityScheme = new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                // Add security requirements globally.  If needs to be unique per operation then use IOperationFilter.
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = "Authorization",
                                Type = SecuritySchemeType.ApiKey,
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                             },
                             new string[] {}
                            }
                });
            });

            return services;
        }
    }
}
