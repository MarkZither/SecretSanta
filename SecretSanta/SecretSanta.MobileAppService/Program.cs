using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace SecretSanta.MobileAppService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
                .UseApplicationInsights()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
