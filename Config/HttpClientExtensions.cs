using System.Text;
using System.Text.Json;
using Helper.APIClient;

namespace Config
{
	public static class HttpClientExtensions
	{
		private static Dictionary<string, string> URLList = new Dictionary<string, string>()
		{
			["IFINBASE"] = "http://147.139.191.88:8000",
			["IFINSYS"] = "http://147.139.191.88:7000/ifinsys",
			["IFINCORE"] = "http://147.139.191.88:8002",
			["IFINCMS"] = "http://147.139.191.88:8003",
			["IFINSLIK"] = "http://localhost:5198",
			["IFINSIPP"] = "http://147.139.191.88:8005"
		};

		public static void AddAPIClient(this IServiceCollection services)
		{
			services.AddTransient<HeaderHandler>(_ => new HeaderHandler("bmltZEE="));

			services.AddHttpClient<IFINSYSClient>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IFINSYS") ?? URLList["IFINSYS"]));
			services.AddHttpClient<IFINBASEClient>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IFINBASE") ?? URLList["IFINBASE"]));
			services.AddHttpClient<IFINCMSClient>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IFINCMS") ?? URLList["IFINCMS"]));
			services.AddHttpClient<IFINCOREClient>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IFINCORE") ?? URLList["IFINCORE"]));
			services.AddHttpClient<IFINSIPPClient>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IFINSIPP") ?? URLList["IFINSIPP"]));
			services.AddHttpClient<IFINSLIKClient>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IFINSLIK") ?? URLList["IFINSLIK"]));
		}

		public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T data)
			 => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data ?? default!) });

		public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T data, CancellationToken cancellationToken)
			 => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data ?? default!) }, cancellationToken);

		public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
			 => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data ?? default!) });

		public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data, CancellationToken cancellationToken)
			 => httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = Serialize(data ?? default!) }, cancellationToken);

		private static HttpContent Serialize(object data) => new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
	}

	public class HeaderHandler(string _headerValue) : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request.Headers.Add("User", _headerValue);
			return await base.SendAsync(request, cancellationToken);
		}

	}
}