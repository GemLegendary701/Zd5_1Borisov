using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zd5_Borisov.Services;
using zd5_Borisov.Views;

namespace zd5_Borisov
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Welcome());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
