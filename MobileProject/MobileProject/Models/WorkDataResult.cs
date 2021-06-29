using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProject.Models
{
   public class WorkDataResult
    {
        public List<Work> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
