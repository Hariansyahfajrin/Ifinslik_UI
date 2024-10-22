using Data.Model;
using System.Text.Json;
using Radzen;
using System.Reflection;
using Config;
using System.Text;
using Helper.Auth;

namespace Helper.APIClient
{
	public class BaseHttpClient
	{
		protected readonly HttpClient _httpClient;
		private readonly NotificationService _notificationService;
		private readonly DialogService _dialogService;
		private readonly ILogger _logger;

		public BaseHttpClient(HttpClient httpClient, AuthStateProvider authStateProvider, NotificationService notificationService, DialogService dialogService, ILogger logger)
		{
			_httpClient = httpClient;
			_notificationService = notificationService;
			_dialogService = dialogService;
			_logger = logger;
		}

		public async Task<BodyResponse<List<T>>?> GetRows<T>(string controller, string route, object? parameters = null)
		{
			string uri = $"api/{controller}/{route}";

			try
			{
				return await SendAsync<List<T>>(new RequestParam
				{
					Method = HttpMethod.Get,
					Endpoint = uri,
					Data = parameters,
				});
			}
			catch (Exception ex)
			{
				Notify(ex.Message, NotificationSeverity.Warning);
				return new();
			}
		}

		public async Task<BodyResponse<T>?> GetRow<T>(string controller, string route, object? parameters)
		{
			string uri = $"api/{controller}/{route}";

			return await SendAsync<T>(new RequestParam
			{
				Method = HttpMethod.Get,
				Endpoint = uri,
				Data = parameters,
			});
		}

		public async Task<BodyResponse<T>?> GetRow<T>(string controller, string route, string? id)
		{
			return await GetRow<T>(controller, route, new { ID = id });
		}

		public async Task<BodyResponse<BaseModel>?> Post(string controller, string route, object data)
		{
			// objects = RemoveModelFromObject(objects);

			string uri = $"api/{controller}/{route}";

			HttpResponseMessage res = await SendAsync(new RequestParam
			{
				Method = HttpMethod.Post,
				Endpoint = uri,
				Data = data,
				DataAsParam = false
			});

			var contentString = await res.Content.ReadAsStringAsync();

			var tempContent = JsonSerializer.Deserialize<BodyResponse<object>>(contentString);

			// Check the Result property
			if (tempContent?.Result == 0)
			{
				var errorResponse = new BodyResponse<BaseModel>
				{
					Result = tempContent.Result,
					Status = tempContent.Status,
					Message = tempContent.Message,
					Data = null
				};
				NotifyResult(errorResponse);
				return errorResponse;
			}

			var content = JsonSerializer.Deserialize<BodyResponse<BaseModel>>(contentString);
			NotifyResult(content);
			return content;
		}

		public async Task<BodyResponse<object>?> Put(string controller, string route, object data)
		{
			data = data.GetType()
							.GetProperties(BindingFlags.Public | BindingFlags.Instance)
							.Where((prop) => !prop.PropertyType.ToString().Contains("Data.Model."))
							.ToDictionary(prop => prop.Name, prop => prop.GetValue(data));

			string uri = $"api/{controller}/{route}";

			var content = await SendAsync<object>(new RequestParam
			{
				Method = HttpMethod.Put,
				Endpoint = uri,
				Data = data,
				DataAsParam = false
			});


			NotifyResult(content);

			return content;
		}
		public async Task<BodyResponse<object>?> Delete(string controller, string route, object data)
		{
			var uri = $"api/{controller}/{route}";

			var content = await SendAsync<object>(new RequestParam
			{
				Method = HttpMethod.Delete,
				Endpoint = uri,
				Data = data,
				DataAsParam = false
			});

			NotifyResult(content);

			return content;
		}

		public async Task<HttpResponseMessage> SendAsync(RequestParam param)
		{
			string uri = $"{GetBaseUrl()}{param.Endpoint}";
			var request = new HttpRequestMessage();

			if (param.DataAsParam || param.Method == HttpMethod.Get)
			{
				uri += await MakeQueryParams(param.Data);
			}
			else
			{
				var jsonData = JsonSerializer.Serialize(param.Data);
				request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			}

			request.RequestUri = new Uri(uri);
			request.Method = param.Method;

			foreach (var header in param.Headers)
			{
				request.Headers.Add(header.Key, header.Value);
			}

			Console.WriteLine($"Request: {request.ToString()}");
			if (request.Content != null)
			{
				Console.WriteLine($"Content: {await request.Content.ReadAsStringAsync()}");
			}
			Console.WriteLine();

			HttpResponseMessage response = await _httpClient.SendAsync(request);

			Console.WriteLine($"Response: {response.ToString()}");
			if (response.Content != null)
			{
				Console.WriteLine($"Content: {await response.Content.ReadAsStringAsync()}");
			}
			Console.WriteLine();

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception(response.ReasonPhrase);
			}


			return response;
		}

		public async Task<BodyResponse<T>?> SendAsync<T>(RequestParam param)
		{
			var res = await SendAsync(param);

			try
			{
				return JsonSerializer.Deserialize<BodyResponse<T>>(res.Content.ReadAsStringAsync().Result);
			}
			catch (System.Exception)
			{
				throw new Exception("Failed Binding Content to " + typeof(T).Name);
			}

		}

		public string? GetBaseUrl()
		{
			return _httpClient?.BaseAddress?.ToString();
		}

		private void NotifyResult<T>(BodyResponse<T>? res)
		{
			if (res?.Result >= 1)
			{
				Notify(res.Message, NotificationSeverity.Success);
			}
			else
			{
				Notify(res?.Message ?? "Something went wrong", NotificationSeverity.Error);
			}
		}

		private void Notify(string message, NotificationSeverity severity)
		{
			switch (severity)
			{
				case NotificationSeverity.Success:
					_notificationService.Notify(new NotificationMessage
					{
						Severity = NotificationSeverity.Success,
						Summary = "Success",
						Duration = 2000
					});
					break;
				case NotificationSeverity.Info:
					_notificationService.Notify(new NotificationMessage
					{
						Severity = NotificationSeverity.Info,
						Summary = "Info",
						Duration = 2000
					});
					break;
				case NotificationSeverity.Warning:
					_notificationService.Notify(new NotificationMessage
					{
						Severity = NotificationSeverity.Warning,
						Summary = "Warning",
						Duration = 2000,
						Detail = message
					});

					break;
				case NotificationSeverity.Error:
					_notificationService.Notify(new NotificationMessage
					{
						Severity = NotificationSeverity.Error,
						Summary = "Error",
						Duration = 2000
					});
					_dialogService.Alert(message, "Error");
					break;
			}
		}

		private static object RemoveModelFromObject(object obj)
		{
			return obj.GetType()
							.GetProperties(BindingFlags.Public | BindingFlags.Instance)
							.Where((prop) => !prop.PropertyType.ToString().Contains("Data.Model."));
		}

		private static async Task<string> MakeQueryParams(object? obj)
		{
			obj = obj?.GetType().GetProperties().ToDictionary(key => key.Name, prop => prop.GetValue(obj)?.ToString());
			var json = JsonSerializer.Serialize(obj);
			var queryParams = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

			if (queryParams != null)
			{
				var formUrlEncoded = new FormUrlEncodedContent(queryParams);
				var queryString = await formUrlEncoded.ReadAsStringAsync();
				return $"?{queryString}";
			}

			return "";
		}
	}

	public class RequestParam()
	{
		public HttpMethod Method { get; set; } = HttpMethod.Get;
		public string Endpoint { get; set; } = "";
		public object? Data { get; set; }
		public bool DataAsParam { get; set; } = false;
		public Dictionary<string, string> Headers { get; set; } = new()
		{
			["User"] = "bmltZEE="
		};
	}
}