
using FormsControls.Base;
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
    public partial class GetStartedPage : ContentPage
    {
        List<string> list;


        public GetStartedPage()
        {
            InitializeComponent();
             list = new List<string>
            {
                "Uygulama",
                "Başlangıç Ekranı"
            };
            deneme.ItemsSource = list;
            deneme.BindingContext = list;
        }
        async void OnGetStartedTapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GetStartedPage());
        }

        private async void nextButton_Clicked(object sender, EventArgs e)
        {
            if(deneme.Position+1!=list.Count)
            {
                deneme.Position = deneme.Position + 1;
            }
            
            else 
            {
                await Navigation.PushModalAsync(new Login());
            }
        }

       private async void skipButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Login());
          //await Navigation.PushAsync(new AnimationNavigationPage(new Login()));

            
        }

    }
}