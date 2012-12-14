using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Client_ASP.Models
{
    public class CreateFeedModel
    {
        [Required]
        [Display(Name = "Feed Address")]
        public string Uri { get; set; }
    }
}