using MobileProject.Models;
using MobileProject.ServiceProvider;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWork : Rg.Plugins.Popup.Pages.PopupPage
    {
       private List<Task> taskList = new List<Task>();
        public AddWork()
        {
            InitializeComponent();
        }

        private async void PublishWork(object sender, EventArgs e)
        {
            Work work = new Work();
            work.Tittle = WorkTitleEntry.Text;
            work.UserId = GetUserProfile.GetUser().Id;
            work.Category = WorkCategoryEntry.Text;
            work.CompanyMail = WorkCompanyEmailEntry.Text;
            work.CompanyName = WorkCompanyNameEntry.Text;
            work.CompanyPhone = WorkCompanyPhoneEntry.Text;
            work.EducationStatus = WorkEducationStatusEntry.Text;
            work.Experience = WorkExperienceEntry.Text;
            work.Explanation = WorkExplanationEntry.Text;
            work.PositionLevel = WorkPositionLevelEntry.Text;
            work.Hıw = WorkHıwEntry.Text;
            work.Address = WorkAddressEntry.Text;

            var locations = await Geocoding.GetLocationsAsync(work.Address);
            var location = locations?.FirstOrDefault();
            work.AddressLatitude = location.Latitude;
            work.AddressLongitude = location.Longitude;


            WorkProvider workProvider = new WorkProvider();
            Task<Result> workTask = Task<Result>.Run(() => workProvider.AddWork(work));
            taskList.Add(workTask);
            Task.WaitAll(taskList.ToArray());

            if (workTask.Status == TaskStatus.RanToCompletion)
            {
                
                var result = workTask.Result;
                if(result.Success)
                {
                    DisplayAlert(result.Message, "", "tamam");
                }
                else
                {
                    DisplayAlert(result.Message, "", "tamam");
                }
                

            }


            //await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new NavbarBottom()); // yenisini ekle
                // geri tuşuna basılınca önceki sayfa açılıyor kapat
            
            await PopupNavigation.Instance.PopAsync();

            

        }
    }
}