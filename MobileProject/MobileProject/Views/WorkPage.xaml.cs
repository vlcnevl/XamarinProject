using MobileProject.Models;
using MobileProject.ServiceProvider;
using Rg.Plugins.Popup.Services;
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
    public partial class WorkPage : ContentPage
    {
        List<Work> works = new List<Work>();
        List<Task> taskList = new List<Task>();
        public WorkPage()
        {
            InitializeComponent();
            GetWorks(works);
            workList.BindingContext = works;
        }

        private async void GetWorks(List<Work> list)
        {
            WorkProvider workProvider = new WorkProvider();
            Task<WorkDataResult> workTask = Task<WorkDataResult>.Run(() => workProvider.GetAllWork());
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

            //string deneme = works[item].Tittle;

            //DisplayAlert(deneme, "", "tamam");

            await PopupNavigation.Instance.PushAsync(new WorkDetailPage(works[item]));

        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Work> searchWorks = new List<Work>();

            WorkProvider workProvider = new WorkProvider();
                Task<WorkDataResult> workTask = Task<WorkDataResult>.Run(() => workProvider.GetWorksByTittle(e.NewTextValue));
                taskList.Add(workTask);
                Task.WaitAll(taskList.ToArray());

                if (workTask.Status == TaskStatus.RanToCompletion)
                {

                    var result = workTask.Result;
                    if (result.Success)
                    {
                        searchWorks = result.Data;
                        workList.BindingContext = searchWorks;
                    }
                    else
                    {
                        DisplayAlert(result.Message, "", "tamam");
                    }

                }
                
             if(e.NewTextValue.Length==0)
            {
                workList.BindingContext = works;
            }
            
        }
    }
}