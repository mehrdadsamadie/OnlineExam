using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
    [Table("ExamType", Schema = "Exm")] 
    public class ExamType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamTypeId { get; set; }
        [MaxLength(100)]
        [Display(Name = "Exam type")]
        public string Name { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}
