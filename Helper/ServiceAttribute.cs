namespace Helper
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ServiceAttribute : Attribute
	{
		public ServiceAttribute()
		{
		}
	}

}