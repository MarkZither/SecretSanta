﻿using System;
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
            services.AddSantaServices();

            //services.Configure<MailGunOptions>(Configuration.GetSection(MailGunOptions.MailGun));

            services.AddMvc();
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton<IParticipantRepository, ParticipantRepository>();
            services.AddSingleton<IHistoryRepository, HistoryRepository>();
            services.AddTransient<IEmailService, MailGunService>();

            services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

            app.Run(async (context) => await Task.Run(() => context.Response.Redirect("/swagger")));
        }
    }
}
