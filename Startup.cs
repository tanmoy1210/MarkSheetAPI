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
using Microsoft.OpenApi.Models;


using System.Net.Mime;
using System.Text;
#if InvalidModelStateResponseFactory
using System.Net.Mime;
#endif

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
//#if ExceptionFilter
using MT.Filters;
using WebEssentials.AspNetCore.Pwa;


namespace MT
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllersWithViews();
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            #region JwtAuthentication

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            //});

            services.AddCors();
            services.AddControllers();

            var key = "This is my private key";

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));

            #endregion

            string conn = Configuration.GetConnectionString("MT");

            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<DBLayer.mararkContext>(options => options.UseMySQL(Configuration.GetConnectionString("MT")));
           
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);

                    // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                    return result;
                }; ;
            });
            //#if InvalidModelStateResponseFactory
            //            #region snippet_DisableProblemDetailsInvalidModelStateResponseFactory
            //                                services.AddControllers()
            //                                    .ConfigureApiBehaviorOptions(options =>
            //                                    {
            //                                        options.InvalidModelStateResponseFactory = context =>
            //                                        {
            //                                            var result = new BadRequestObjectResult(context.ModelState);

            //                                            // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
            //                                            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            //                                            result.ContentTypes.Add(MediaTypeNames.Application.Xml);

            //                                            return result;
            //                                        };
            //                                    });
            //            #endregion
            //#endif

            //#if ExceptionFilter
            //            #region snippet_AddExceptionFilter
            //                        services.AddControllers(options =>
            //                            options.Filters.Add(new HttpResponseExceptionFilter()));
            //            #endregion
            //#endif

            services.AddMvc(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                foreach (var formatter in options.InputFormatters)
                {
                    if (formatter.GetType() == typeof(Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter))
                        ((Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter)formatter).SupportedMediaTypes.Add(
                        Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain"));
                }
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            //services.AddProgressiveWebApp(new PwaOptions
            //{
            //    CacheId = "Worker " + "1.1",
            //    Strategy = ServiceWorkerStrategy.CacheFirst,
            //    RoutesToPreCache = "/Home/Index",

            //    OfflineRoute = "Offline/Index",
            //});

            #region Register Swagger            
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Marksheet API",
                    Description = "Web API Application for School ERP",
                });
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // app.UseAuthorization();
            //web ke abhi 7/17/2021
            app.UseAuthentication();

            app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowCredentials()); // allow credentials

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            #region Bind and Set Swagger
            // Bind to swagger options
            var swaggerOptions = new SwaggerOptions.SwaggerOptions();
            Configuration.GetSection(nameof(swaggerOptions)).Bind(swaggerOptions);

            // Set the swagger json route
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });

            // Set the swagger UI
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });
            #endregion

            #region Add Log4Net
            loggerFactory.AddLog4Net();
            #endregion

        }
    }
   
}
