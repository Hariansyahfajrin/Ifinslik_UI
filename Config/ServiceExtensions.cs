using System.Reflection;
using Helper;
using Radzen;

namespace Config
{
	public static class ServiceExtensions
	{
		public static void AddService(this IServiceCollection services)
		{
			services.AddScoped<NotificationService>();
			services.AddScoped<DialogService>();
			services.AddScoped<LoadingService>();

			foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
			{
				var customAttributes = type.GetCustomAttributes(typeof(ServiceAttribute), true);
				if (customAttributes.Length > 0)
				{
					// Tambahkan ke dalam scope (misalnya, dengan AddScoped)
					Console.WriteLine($"Registering service: {type.Name}");
					services.AddScoped(type);
				}
			}

		}
	}


}
