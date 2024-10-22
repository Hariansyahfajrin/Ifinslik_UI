namespace Helper.APIClient
{
	public class LoggingHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// Log detail request
			Console.WriteLine($"Request: {request.ToString()}");
			if (request.Content != null)
			{
				Console.WriteLine($"Content: {await request.Content.ReadAsStringAsync()}");
			}
			Console.WriteLine();

			// Panggil handler berikutnya dalam pipeline
			HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

			// Log detail response
			Console.WriteLine($"Response: {response.ToString()}");
			if (response.Content != null)
			{
				Console.WriteLine($"Content: {await response.Content.ReadAsStringAsync()}");
			}
			Console.WriteLine();

			return response;
		}
	}

}