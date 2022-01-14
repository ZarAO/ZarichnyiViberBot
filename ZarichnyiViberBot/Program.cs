using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace ZarichnyiViberBot
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			IHostBuilder builder = Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				})
				.ConfigureWebHostDefaults(webBuider =>
				{
					webBuider.UseStartup<Startup>();
				});

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				builder.UseWindowsService();
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				builder.UseSystemd();
			}

			return builder;
		}
	}
}
