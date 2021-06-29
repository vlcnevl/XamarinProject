using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProject.Models
{
    public class WorkReferenceDataResult
    {
        public List<WorkReference> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
