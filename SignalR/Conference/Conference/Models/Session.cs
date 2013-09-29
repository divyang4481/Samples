using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conference.Models
{
    public class Session
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Abstract { get; set; }

        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}