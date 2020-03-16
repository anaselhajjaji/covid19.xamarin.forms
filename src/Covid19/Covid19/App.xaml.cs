using Xamarin.Forms;
using Covid19.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Covid19
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CasesView();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=2f72f366-9e64-4206-9a40-fbece5075d31;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}