using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProject.Models
{
   public class UserDataResults
    {
        public User Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
