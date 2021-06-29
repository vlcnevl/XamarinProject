using Entities.DTOs;
using FormsControls.Base;
using MobileProject.Models;
using MobileProject.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public static string Token;
        List<Task> taskList = new List<Task>();
       
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            UserForLoginDto userForLogin = new UserForLoginDto();
            userForLogin.Email = UserEmailEntry.Text;
            userForLogin.Password = UserPasswordEntry.Text;
            App.Email = UserEmailEntry.Text;
            AuthProvider provider = new AuthProvider();

            Task<LoginRegisterResult> loginTask = Task<LoginRegisterResult>.Run(() => provider.Login(userForLogin).Result);
            taskList.Add(loginTask);
            Task.WaitAll(taskList.ToArray());
           

            if (loginTask.Status == TaskStatus.RanToCompletion)
            {
                var result = loginTask.Result;
                
                if(result.Success)
                {
                    Token = result.Token;
                    await Navigation.PushModalAsync(new NavbarBottom());
                }
                else
                {
                    DisplayAlert(result.Message, "", "Tamam");
                } 
            }

            //UserProvider userProvider = new UserProvider();
            //Task<Results> userTask = Task<Results>.Run(() => userProvider.GetByEmail(App.Email).Result);
            //taskList.Add(userTask);
            //Task.WaitAll(taskList.ToArray());

            //if (userTask.Status == TaskStatus.RanToCompletion)
            //{
            //    var result = userTask.Result;

            //    if (result.Success)
            //    {
            //        App.user = result;
                  
            //    }
            //    else
            //    {
            //        DisplayAlert("sdaf", "sdf", "sdf");
            //    }
            //}

        }

        private async void SignupButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Signup());
        }


    }
}