using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Examination.Domain.Entities
{
    [Table("Exam", Schema = "Exm")]
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }
        [Required(ErrorMessage = "The exam name Reqiured")]
        [MaxLength(100)]
        [Display(Name = "Exam name")]
        public string Name { get; set; }
        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("ParentExam")]
        [Display(Name = "Exam Required")]
        public int? ParentExamId { get; set; }
        [ForeignKey("ExamType")]
        public int ExamTypeId { get; set; }
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Delete")]
        public bool IsDelete { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public virtual ExamType ExamType { get; set; }
        public ICollection<Question> Questions { get; set; }
        public virtual Exam ParentExam { get; set; }

    }
}
