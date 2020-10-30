using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examination.WebUI.Models
{
    public class UserExamResultView
    {
        public int QuestionId { get; set; }
        [AllowHtml]
        public string QuestionName { get; set; }
        public int NumberAnswer { get; set; }
        public string SelectedAnswer { get; set; }
        public decimal? QuestionPoint { get; set; }
        public TimeSpan? duration { get; set; }
    }
}