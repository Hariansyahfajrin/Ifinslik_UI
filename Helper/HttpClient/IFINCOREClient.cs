using Helper.Auth;
using Radzen;

namespace Helper.APIClient
{
	public class IFINCOREClient(HttpClient httpClient, AuthStateProvider authStateProvider, NotificationService notificationService, DialogService dialogService, ILogger<BaseHttpClient> logger) : BaseHttpClient(httpClient, authStateProvider, notificationService, dialogService, logger)
	{
	}
}