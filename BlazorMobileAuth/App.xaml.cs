using BlazorMobile.Common;
using BlazorMobile.Components;
using BlazorMobileAuth.Helpers;
using BlazorMobile.Services;

namespace BlazorMobileAuth
{
	public partial class App : BlazorApplication
    {
        public App()
        {
            InitializeComponent();

            ServiceRegistrationHelper.RegisterServices();

            WebApplicationFactory.SetHttpPort(8888);

            MainPage = new MainPage();
        }
    }
}
