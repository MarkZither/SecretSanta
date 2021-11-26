using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SecretSanta.Models;
using SecretSanta.MobileAppService.Services;
using SecretSanta.MobileAppService.Options;
using SecretSanta.MobileAppService.Extensions;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling.Storage;
using System.Collections.Generic;

namespace SecretSanta.MobileAppService
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            //https://github.com/dotnet/runtime/issues/38491
            //services.AddOptions<MailGunOptions>()
            //.Bind(Configuration.GetSection(MailGunOptions.MailGun))
            //.ValidateDataAnnotations();

            //services.ConfigureOptions<MailGunOptions>();
            //services.AddTransient<IValidateOptions<SantaOptions>, ConfigureSantaOptions>();
            services.AddSantaServices();services.AddMemoryCache(); 
            services.AddMiniProfiler(options => { options.RouteBasePath = "/profiler";
                // Note: MiniProfiler will not work if a SizeLimit is set on MemoryCache!
                //   See: https://github.com/MiniProfiler/dotnet/issues/501 for details
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
            }).AddEntityFramework();
            //services.Configure<MailGunOptions>(Configuration.GetSection(MailGunOptions.MailGun));

            services.AddMvc();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.ClientId = "interactive";
                    options.MapInboundClaims = false;
                    options.SaveTokens = true;
                });
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton<IParticipantRepository, ParticipantRepository>();
            services.AddSingleton<IHistoryRepository, HistoryRepository>();
            services.AddTransient<IEmailService, MailGunService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SecretSanta Service", Version = "v1" });
                c.OperationFilter<AuthorizeOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api1", "Demo API - full access"}
                            }
                        }
                    }
                });
            });


            services
                .AddFluentEmail("santa@secretsanta.mark-burton.com")
                .AddRazorRenderer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiniProfiler();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.OAuthClientId("demo_api_swagger");
                options.OAuthAppName("Demo API - Swagger");
                options.OAuthUsePkce();
            });
        }
    }
}
