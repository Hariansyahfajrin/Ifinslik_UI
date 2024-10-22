using System.Net;

namespace Helper
{
	public class BodyResponse<T>
	{
		public int Result { get; set; } = -1;
		public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
		public string Message { get; set; } = "Something went wrong";
		public T? Data { get; set; }

	}
}
