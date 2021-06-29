using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string CvPath { get; set; }
        public string AboutMe { get; set; }
    }
}