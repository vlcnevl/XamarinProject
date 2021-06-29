using MobileProject.Models;
using MobileProject.ServiceProvider;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MobileProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        List<Task> taskList = new List<Task>();
        public MapPage()
        {
            InitializeComponent();

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(39.925, 32.836944), Distance.FromKilometers(10)));
            this.BindingContext = this;
            GetWorks();
            

        }
        public ObservableCollection<Album> MyImages { get; set; }
        public class Album
        {
            public string Image { get; set; }
            public string Description { get; set; }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AddWork());
        }

        
        private async void  GetWorks()
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
                    foreach(Work work in result.Data)
                    {
                        Pin pin = new Pin
                        {
                            Type = PinType.Place,
                            Label = work.Tittle,
                            Position = new Position(work.AddressLatitude, work.AddressLongitude)
                           
                        };
                        //pin.MarkerClicked += markerClicked();
                        pin.MarkerClicked += async (s, args) =>
                         {
                           // await DisplayAlert(work.AddressLatitude.ToString(), "", "tamam");
                             await PopupNavigation.Instance.PushAsync(new WorkDetailPage(work));
                         };


                        MyMap.Pins.Add(pin);





                    }

                }
                else
                {
                    DisplayAlert(result.Message, "", "tamam");
                }

            }
          
        }

        //async private void markerClicked(object sender, EventArgs e)
        //{
        //   await PopupNavigation.Instance.PushAsync(new AddWork());
        //}
    }
}