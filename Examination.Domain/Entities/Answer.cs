using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Examination.Domain.Entities
{
    [Table("Answer", Schema = "Exm")] 
    public class Answer
    {
        private const int DEFAULT_ORDER = 0;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }
        [ForeignKey("Question")]
        [Required]
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Answer")]
        public string Text { get; set; }
        [DefaultValue(DEFAULT_ORDER)]
        [Display(Name= "Order")]
        public int Order { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserScore> UserScores { get; set; }

    }
}
