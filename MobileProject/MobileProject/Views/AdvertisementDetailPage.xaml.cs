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
    public partial class AdvertisementDetailPage : ContentPage
    {
        List<User> users = new List<User>();
        List<Task> taskList = new List<Task>();
        public AdvertisementDetailPage(Work  work)
        {
            InitializeComponent();

            GetUsers(work.Id);
            usersList.BindingContext = users;
            
        }

        private void GetUsers(int workId)
        {
            WorkReferenceProvider workProvider = new WorkReferenceProvider();
            Task<WorkDetailDataResult> workReferenceTask = Task<WorkDetailDataResult>.Run(() => workProvider.GetUserWithWorkId(workId));
            taskList.Add(workReferenceTask);
            Task.WaitAll(taskList.ToArray());

            if (workReferenceTask.Status == TaskStatus.RanToCompletion)
            {

                var result = workReferenceTask.Result;
                if (result.Success)
                {

                    users = result.Data;
                }
                else
                {
                    DisplayAlert(result.Message, "", "tamam");
                }

            }
        }
    }
}