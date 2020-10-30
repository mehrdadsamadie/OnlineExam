using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examination.WebUI.Models
{
    public class UserExamsResultView
    {
        public int ExamId { get; set; }
        
        public string ExamName { get; set; }

        public string ExamType { get; set; }

        public decimal? ExamPoint { get; set; }
        
    }
}