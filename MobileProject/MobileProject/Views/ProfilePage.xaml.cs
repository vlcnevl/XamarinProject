using MobileProject.Models;
using MobileProject.ServiceProvider;
using Rg.Plugins.Popup.Pages;
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
    public partial class ProfilePage : ContentPage
    {
        string date;
        string cvPath;
        List<Task> taskList = new List<Task>();
        public ProfilePage()
        {
            InitializeComponent();
            userEmailLabel.Text = GetUserProfile.GetUser().Email;
            UserNameEntry.Text = GetUserProfile.GetUser().FirstName;
            UserLastNameEntry.Text = GetUserProfile.GetUser().LastName;
        }

        private async void ProfilePhotoClicked(object sender, EventArgs e)
        {
           
            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
               
                PickerTitle = "Profil fotoğrafı seç"
            });

            if (pickResult != null)
            {
                var stream = await pickResult.OpenReadAsync();
                profileImage.Source = ImageSource.FromStream(() => stream);
            }

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            date = UserBirthDate.Date.ToString();

        }

        private async void AddCv_Clicked(object sender, EventArgs e)
        {

            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Pdf,
                //FileTypes = customFileType,
                PickerTitle = "Cv Seç"
            });

            if (pickResult != null)
            {
                var stream = await pickResult.OpenReadAsync();
                 resultImage.Source = FileImageSource.FromStream(() => stream);
                
            }
        }

        private void SaveProfile_Clicked(object sender, EventArgs e)
        {
            User user = new User();

            user.Id = GetUserProfile.GetUser().Id;
            user.PasswordHash = GetUserProfile.GetUser().PasswordHash;
            user.PasswordSalt = GetUserProfile.GetUser().PasswordSalt;
            user.Status = GetUserProfile.GetUser().Status;
            user.Email = GetUserProfile.GetUser().Email;
            user.AboutMe = UserAboutMeEntry.Text;
            user.Address = UserAddressEntry.Text;
            user.BirthDate = date;
            user.CvPath = "asdfaf";
            user.Mobile = UserTelephoneEntry.Text;
            user.UserName = UserNickNameEntry.Text;
            if(UserNameEntry.Text.Length==0)
            {
                user.FirstName = GetUserProfile.GetUser().FirstName;
            }
            else
            {
                user.FirstName = UserNameEntry.Text;
            }
            if (UserLastNameEntry.Text.Length==0)
            {
                user.LastName = GetUserProfile.GetUser().LastName;
            }
            else
            {
                user.LastName = UserLastNameEntry.Text;
            }


            UserProvider userProvider = new UserProvider();
            Task<Result> userTask = Task<Result>.Run(() => userProvider.UpdateUser(user));
            taskList.Add(userTask);
            Task.WaitAll(taskList.ToArray());

            if (userTask.Status == TaskStatus.RanToCompletion)
            {
                var result = userTask.Result;
                DisplayAlert("Bilgiler Güncellendi", "", "tamam");
                
            }








        }



    }
}