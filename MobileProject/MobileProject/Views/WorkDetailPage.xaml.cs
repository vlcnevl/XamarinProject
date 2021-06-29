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
    public partial class WorkDetailPage : Rg.Plugins.Popup.Pages.PopupPage
    {
       private List<Task> taskList = new List<Task>();
        private int workId;
        private int workUserId;
        public WorkDetailPage(Models.Work work)
        {
            InitializeComponent();
            WorkTittleLabel.Text = work.Tittle.ToUpper();
            WorkCompanyNameLabel.Text = work.CompanyName;
            WorkCategoryLabel.Text = work.Category;
            WorkHıwLabel.Text = work.Hıw;
            WorkPositionLevelLabel.Text = work.PositionLevel;
            WorkEducationStatusLabel.Text = work.EducationStatus;
            WorkExperienceLabel.Text = work.Experience;
            WorkAddressLabel.Text = work.Address;
            WorkCompanyPhoneLabel.Text = work.CompanyPhone;
            WorkCompanyMailLabel.Text = work.CompanyMail;
            WorkExplanationLabel.Text = work.Explanation;
            workId = work.Id;
            workUserId = work.UserId;
            
        }

        private async void MessageButton_Clicked(object sender, EventArgs e)
        {  
            await Navigation.PushModalAsync(new MessagePage());
            await PopupNavigation.Instance.PopAsync();
           App.workUserEmail =  GetUserProfile.GetUserById(workUserId);
           
        }

        private void ApplyButton_Clicked(object sender, EventArgs e) // bi else daha ekle aynı ilana iki kere başvurmasın
        {
            WorkReference workReference = new WorkReference();
            workReference.UserId = GetUserProfile.GetUser().Id;
            workReference.WorkId = workId;


            if(workUserId == GetUserProfile.GetUser().Id)
            {
                DisplayAlert("Kendi yayımlamış olduğuz ilana başvuramazsınız!", "", "Tamam");
            }
            else
            {
                WorkReferenceProvider workReferenceProvider = new WorkReferenceProvider();
                Task<Result> workTask = Task<Result>.Run(() => workReferenceProvider.AddWorkReferences(workReference));
                taskList.Add(workTask);
                Task.WaitAll(taskList.ToArray());

                if (workTask.Status == TaskStatus.RanToCompletion)
                {

                    var result = workTask.Result;
                    if (result.Success)
                    {
                        DisplayAlert(result.Message, "", "tamam");
                    }
                    else
                    {
                        DisplayAlert(result.Message, "", "tamam");
                    }

                }

            }
        }
    }
}