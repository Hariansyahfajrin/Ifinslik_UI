namespace Helper
{
	public class LoadingService
	{
		public event Action? OnShow;
		public event Action? OnClose;
		public bool IsLoading { get; private set; } = false;

		public void Show()
		{
			IsLoading = true;
			OnShow?.Invoke();
		}
		public void Close()
		{
			IsLoading = false;
			OnClose?.Invoke();
		}
	}
}