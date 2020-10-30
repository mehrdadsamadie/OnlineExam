using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.ComponentModel;

namespace Examination.Domain.Entities
{
    [Table("Question", Schema = "Exm")]
    public class Question
    {
        private const int DEFAULT_ORDER = 0;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        [Required]
        [Display(Name = "Question")]
        [AllowHtml]
        public string Text { get; set; }
        [ForeignKey("Exam")]
        [Display(Name="Exam")]
        [Required]
        public int ExamId { get; set; }

        [ForeignKey("Answer")]
        public int? CorrectAnswerId { get; set; }
        [DefaultValue(DEFAULT_ORDER)]
        [Display(Name = "Order")]
        public int Order { get; set; }
        [Display(Name = "Point")]
        public decimal? Score { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Guide> Guides { get; set; }
        public virtual ICollection<UserScore> UserScores { get; set; }

    }
}
