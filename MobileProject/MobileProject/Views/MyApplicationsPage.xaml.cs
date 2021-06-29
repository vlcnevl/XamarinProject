using MobileProject.Models;
using MobileProject.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyApplicationsPage : ContentPage
    {
        List<Work> works = new List<Work>();
        List<Task> taskList = new List<Task>();
        public MyApplicationsPage()
        {
            InitializeComponent();
            GetAllWorkReferenceByUserId(GetUserProfile.GetUser().Id);
            workList.ItemsSource = works;

        }

        private async void GetAllWorkReferenceByUserId(int userId)
        {
            WorkReferenceProvider workReferenceProvider = new WorkReferenceProvider();
            Task<WorkDataResult> workTask = Task<WorkDataResult>.Run(() => workReferenceProvider.GetMyApplications(userId));
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



    }
}