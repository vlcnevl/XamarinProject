using MobileProject.Models;
using MobileProject.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisementPage : ContentPage
    {
        List<Work> works = new List<Work>();
        List<Task> taskList = new List<Task>();
        public AdvertisementPage()
        {
            InitializeComponent();
            GetAllWorkByUserId(GetUserProfile.GetUser().Id);
             workList.ItemsSource = works;


        }
        private async void GetAllWorkByUserId(int userId)
        {
            WorkProvider workProvider = new WorkProvider();
            Task<WorkDataResult> workTask = Task<WorkDataResult>.Run(() => workProvider.GetAllWorkByUserId(userId));
            taskList.Add(workTask);
            Task.WaitAll(taskList.ToArray());

            if (workTask.Status == TaskStatus.RanToCompletion)
            {

                var result = workTask.Result;
                if (result.Success)
                {

                    works = result.Data;
                }
                else
                {
                    DisplayAlert(result.Message, "", "tamam");
                }

            }

        }

        private async void workList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItemIndex;

            string deneme = works[item].Tittle;

            //DisplayAlert(deneme, "", "tamam");

            await Navigation.PushModalAsync(new AdvertisementDetailPage(works[item]));


        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = (SwipeItem)sender;
            var param = (Work)item.CommandParameter;
            DisplayAlert(param.Tittle, "Başlıklı ilan kaldırıldı", "Tamam");

            Work work = new Work();
            work.Id = param.Id;
            work.UserId = param.UserId;
            work.Tittle = param.Tittle;
            work.Hıw = param.Hıw;
            work.Explanation = param.Explanation;
            work.Experience = param.Experience;
            work.EducationStatus = param.EducationStatus;
            work.CompanyPhone = param.CompanyPhone;
            work.CompanyName = param.CompanyName;
            work.CompanyMail = param.CompanyMail;
            work.Category = param.Category;
            work.AddressLongitude = param.AddressLongitude;
            work.AddressLatitude = param.AddressLatitude;
            work.Address = param.Address;
            work.PositionLevel = param.PositionLevel;


            for(int i = 0; i < works.Count;i++)
            {
                if(works[i].Id == work.Id)
                {
                    works.RemoveAt(i);
                }
            }



            WorkProvider workProvider = new WorkProvider();
            Task<Result> workTask = Task<Result>.Run(() => workProvider.DeleteWork(work));
            taskList.Add(workTask);
            Task.WaitAll(taskList.ToArray());
         

            workList.ItemsSource = works;
        }
    }
}