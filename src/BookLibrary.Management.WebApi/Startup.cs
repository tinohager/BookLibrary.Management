using BookLibrary.Management.BusinessLogicLayer.AuthorService;
using BookLibrary.Management.BusinessLogicLayer.BookService;
using BookLibrary.Management.BusinessLogicLayer.BorrowService;
using BookLibrary.Management.BusinessLogicLayer.CustomerService;
using BookLibrary.Management.BusinessLogicLayer.PublisherService;
using BookLibrary.Management.DataAccessLayer;
using BookLibrary.Management.MssqlDataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BookLibrary.Management.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(new MssqlConfiguration { ConnectionString = Configuration.GetConnectionString("BookLibraryManagement") });
            services.AddScoped<MssqlInitialize>();

            services.AddScoped<IBookRepository, MssqlBookRepository>();
            services.AddScoped<IAuthorRepository, MssqlAuthorRepository>();
            services.AddScoped<IPublisherRepository, MssqlPublisherRepository>();
            services.AddScoped<ICustomerRepository, MssqlCustomerRepository>();
            services.AddScoped<IBorrowHistoryRepository, MssqlBorrowHistoryRepository>();

            services.AddScoped<IBorrowingFeeCalculation, DefaultBorrowingFeeCalculation>();

            services.AddScoped<IBookService, DefaultBookService>();
            services.AddScoped<IAuthorService, DefaultAuthorService>();
            services.AddScoped<IPublisherService, DefaultPublisherService>();
            services.AddScoped<ICustomerService, DefaultCustomerService>();
            services.AddScoped<IBorrowService, DefaultBorrowService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                    options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = this.Configuration["JwtAuthentication:Issuer"],
                    ValidAudience = this.Configuration["JwtAuthentication:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JwtAuthentication:Key"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", polBuilder => polBuilder.RequireRole("admin"));
            });

            services.AddControllers().AddNewtonsoftJson();//.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookLibrary Management Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            MssqlInitialize mssqlInitialize)
        {
            //Initialize Mapster
            //TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly(), Assembly.Load(new AssemblyName("BookLibrary.Management.BusinessLogicLayer")));

            var databaseReady = mssqlInitialize.InitializeDatabaseAsync().GetAwaiter().GetResult();
            if (!databaseReady)
            {
                throw new TimeoutException("Cannot connect to database");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.EnableFilter();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
