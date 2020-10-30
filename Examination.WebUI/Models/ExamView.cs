using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examination.WebUI.Models
{
    public class ExamView
    {
        public int ExamId { get; set; }
        public int ExamTypeId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
         [Display(Name = "All Question:")]
        public int TotalQuestion { get; set; }
         [Display(Name = "Question Number:")]
        public int TotalQuestionAnswered { get; set; }
        [Display(Name="Exam Point:")]
        public decimal? ExamPoint { get; set; }
        [Display(Name= "Question Point:")]
        public decimal? QuestionPoint { get; set; }
        public int? HintId { get; set; }
        public string Hint { get; set; }
        public DateTime? StartTime { get; set; }
        [Required]
        public int SelctedAnswer { get; set; }
        public List<AnswerStudent> Answers { get; set; }
    }
    public class AnswerStudent
    {
        public int AnswerId { get; set; }
        public string Answer { get; set; }
    }
}