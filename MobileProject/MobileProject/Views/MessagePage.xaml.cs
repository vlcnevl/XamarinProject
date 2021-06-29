using Microsoft.AspNetCore.SignalR.Client;
using MobileProject.Models;
using MobileProject.Models.Interfaces;
using MobileProject.ServiceProvider;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        HubConnection hubConnection;
         ObservableCollection<MessageModel> Messages = new ObservableCollection<MessageModel>();
        private string Name = GetUserProfile.GetUser().Email;
        public MessagePage()
        {
            InitializeComponent();
            messageList.ItemsSource = Messages;
            DoRealTimeSuff();
            sendUserEmail.Text = App.workUserEmail;
        }


        async private void DoRealTimeSuff()
        {
            SignalRChatSetup();
            await SignalRConnect();
        }
        private void SignalRChatSetup()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.1.42:45455/ChatHub").Build();

           //hubConnection.On<string, string>("SendMessage", (user, message) =>
           //{
           //  Messages.Add(new MessageModel() { User = user, Message = message , IsSystemMessage = true, IsOwnMessage = Name == user });
           // });

            hubConnection.On<string, string,string>("SendMessage", (toUser, fromUser,message) =>
            {

                Messages.Add(new MessageModel() { User = fromUser, Message = message, IsSystemMessage = false ,IsOwnMessage=false });
            });
          

        }
        async Task SignalRConnect()
        {
            try
            {
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("SetConnection", Name);
                await hubConnection.InvokeAsync("JoinGroup", Name);

            }
            catch (Exception ex)
            {
                DisplayAlert("Mesajlara bağlanılamadı", "", "Tamam");

            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //await SignalRSendMessage(GetUserProfile.GetUser().Email, messageEntry.Text);
            await SendPrivateMessage(sendUserEmail.Text, GetUserProfile.GetUser().Email,messageEntry.Text);

            messageEntry.Text = "";
 
        }

        async Task SignalRSendMessage(string user, string message)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception ex)
            {
                DisplayAlert("Mesaj Gönderilemedi", "", "Tamam");
            }
        }


        async Task SendPrivateMessage(string toUser, string fromUser, string message)
        {
            Messages.Add(new MessageModel() { User = fromUser, Message = message, IsSystemMessage = false , IsOwnMessage = true });
            try
            {
                await hubConnection.InvokeAsync("SendMessageGroup", toUser,fromUser,message);
            }
            catch (Exception ex)
            {
                DisplayAlert("Mesaj Gönderilemedi", "", "Tamam");
            }

        }


    }
}
