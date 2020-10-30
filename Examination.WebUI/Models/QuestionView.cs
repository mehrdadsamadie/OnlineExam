using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examination.WebUI.Models
{
    public class QuestionView
    {
        public QuestionView (int? examid)
        {
            this.ExamId = examid;
            Answers = new List<AnswerView>(){new AnswerView(),new AnswerView(),new AnswerView(),new AnswerView()};
            Hints = new List<HintView>() {new HintView(),new HintView()};
        }
        public int Id { get; set; }
    //    [UIHint("tinymce_full_compressed")]
        [AllowHtml]
        [Display(Name = "Question")]
        public string Question { get; set; }
        public int? ExamId { get; set; }
        [Display(Name = "Point")]
        public float Point { get; set; }
        [Display(Name = "Order")]
        public int Order { get; set; }
        public IList<AnswerView> Answers { get; set; }
        public IList<HintView> Hints { get; set; } 
    }
}