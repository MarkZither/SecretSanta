using System;
using Xamarin.Forms;
using SecretSanta.Services;
using SecretSanta.Views;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SecretSanta
{
	public partial class App : Application
	{
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "https://secretsantamobileappservice.azurewebsites.net";

        public static bool UseMockDataStore = false;
		
		public App ()
		{
#if DEBUG
            AzureBackendUrl = "https://127.0.0.1:44305";
#endif
            InitializeComponent();

            if (UseMockDataStore)
            {
                DependencyService.Register<MockDataStore>();
                DependencyService.Register<MockMessagingDataStore>();
            }
            else
            {
                DependencyService.Register<AzureDataStore>();
                DependencyService.Register<MessagingDataStore>();
            }

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
