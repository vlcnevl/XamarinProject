using System.Collections.Generic;
using System.Text;
using System;

namespace MobileProject.Models
{
    public class Work
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Tittle { get; set; }
        public string Explanation { get; set; }
        public string Category { get; set; }
        public string Hıw { get; set; }
        public string EducationStatus { get; set; }
        public string Experience { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public double AddressLongitude { get; set; }
        public double AddressLatitude { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyMail { get; set; }
        public string PositionLevel { get; set; }

    }
}
