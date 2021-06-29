using Entities.DTOs;
using MobileProject.Models;
using MobileProject.ServiceProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        public static string Token;
        List<Task> taskList = new List<Task>();
        public Signup()
        {
            InitializeComponent();
        }

        private async void SignupButton_Clicked(object sender, EventArgs e)
        {
            UserForRegisterDto userForRegister = new UserForRegisterDto();
            userForRegister.FirstName = userNameEntry.Text;
            userForRegister.LastName = userLastNameEntry.Text;
            userForRegister.Email = userEmailEntry.Text;
            userForRegister.Password = userPasswordEntry.Text;

            AuthProvider provider = new AuthProvider();

            Task<LoginRegisterResult> registerTask = Task<LoginRegisterResult>.Run(() => provider.Register(userForRegister).Result);
            taskList.Add(registerTask);
            Task.WaitAll(taskList.ToArray());

            if (registerTask.Status == TaskStatus.RanToCompletion)
            {
                var result = registerTask.Result;
                Token = result.Token;
                
                if(result.Success==false)
                {
                    DisplayAlert(result.Message, "", "Tamam");
                }
                await Navigation.PushModalAsync(new Login());
            }

        }
    }
}