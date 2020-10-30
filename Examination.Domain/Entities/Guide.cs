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
    [Table("Guide", Schema = "Exm")]
    public class Guide
    {
        private const int DEFAULT_ORDER = 0;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuideId { get; set; }
        [ForeignKey("Question")]
        [Required]
        public int QuestionId { get; set; }
        //[UIHint("tinymce_full_compressed")]
        [AllowHtml]
        [Required]
        [Display(Name = "Hint")]
        public string Text { get; set; }
        public int? Reduce { get; set; }
        [DefaultValue(DEFAULT_ORDER)]
        [Display(Name = "Order")]
        public int Order { get; set; }
        public Question Question { get; set; }

    }
}
