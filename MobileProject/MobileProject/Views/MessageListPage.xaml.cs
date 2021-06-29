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
    public partial class MessageListPage : ContentPage
    {
        public MessageListPage()
        {
            InitializeComponent();
        }

        private async void PrivateMessageAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MessagePage());
        }
    }
}