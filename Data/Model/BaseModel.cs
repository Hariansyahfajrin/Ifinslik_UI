using System.Text.Json;

namespace Data.Model
{
	public class BaseModel
	{
		public string? ID { get; set; }
		public int? UKey { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}