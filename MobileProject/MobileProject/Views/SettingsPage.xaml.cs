using MobileProject.Models;
using MobileProject.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        List<Task> taskList = new List<Task>();
        public SettingsPage()
        {
            InitializeComponent();
            commandCell.Title = GetUserProfile.GetUser().Email;


        }

        private async void CommandCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProfilePage());
        }

        private async void Logout_Tapped(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new Login());
        }

        private async void myWorks_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AdvertisementPage());
        }

        private async void myApplication_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MyApplicationsPage());
        }
    }
}