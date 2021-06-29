using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MobileProject.Models
{
   public class MessageModel 
    {
        public string User { get; set; }
        public string Message { get; set; }
        public bool IsOwnMessage { get; set; }
        public bool IsSystemMessage { get; set; }
    }
}
