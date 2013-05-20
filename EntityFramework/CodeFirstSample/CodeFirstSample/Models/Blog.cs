using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirstSample.Models
{
    public class Blog
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }

        [Timestamp]
        public DateTime TimeStamp;
    }
}