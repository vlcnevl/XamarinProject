using FormsControls.Base;
using MobileProject.Models;
using MobileProject.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject
{
    public partial class App : Application
    {
        public static string Email { get; set; }
        public static string workUserEmail { get; set; }
       
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Expander_Experimental" });
            MainPage = new NavigationPage(new GetStartedPage());
          //  MainPage = new NavigationPage(new NavbarBottom());

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
