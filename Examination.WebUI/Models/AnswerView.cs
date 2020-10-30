using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examination.WebUI.Models
{
    public class AnswerView
    {
        public AnswerView() 
        { }
        public AnswerView(int id,string answer, int? order, bool iscorrect) 
        {
            this.Id = id;
            this.Answer = answer;
            this.Order = order;
            this.IsCorrect = iscorrect;
        }
        public int Id { get; set; }
    //    [UIHint("tinymce_full_compressed")]
        [Display(Name = "Answer")]

        public string Answer { get; set; }
        [Display(Name = "Order")]
        public int? Order { get; set; }
        public bool IsCorrect { get; set; }
    }
}