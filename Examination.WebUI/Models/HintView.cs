using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examination.WebUI.Models
{
    public class HintView
    {

        public int Id { get; set; }
       // [UIHint("tinymce_full_compressed")]
        [AllowHtml]
        [Display(Name = "Hint")]

        public string Hint { get; set; }
        [Display(Name = "Order")]
        public int Order { get; set; }
    }
}