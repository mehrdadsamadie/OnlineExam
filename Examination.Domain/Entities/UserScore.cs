using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Examination.Domain.Entities
{
    [Table("UserScore", Schema = "Exm")]
    public class UserScore
    {
        private const int DEFAULT_NumberAnswer = 0;
        private const bool DEFAULT_ISFinish = false;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserScoreId { get; set; }
        [ForeignKey("Question")]
        [Required]
        [Index("IX_QuestionAndUser", 1, IsUnique = true)]
        public int QuestionId { get; set; }
        [ForeignKey("User")]
        [Required]
        [Index("IX_QuestionAndUser", 2, IsUnique = true)]
        public string UserId { get; set; }
        [DefaultValue(DEFAULT_NumberAnswer)] 
        public int NumberAnswer { get; set; }
        [ForeignKey("Answer")]
        public int? UserAnswerId { get; set; }
        public decimal? Grade { get; set; }
        [DefaultValue(DEFAULT_ISFinish)]
        public bool IsFinish { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public IdentityUser User { get; set; }

    }
}
