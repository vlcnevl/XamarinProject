using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProject.Models
{
    public class LoginRegisterResult
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
