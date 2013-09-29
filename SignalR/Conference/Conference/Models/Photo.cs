using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Conference.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Picture")]
        public byte[] PhotoFile { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedOn { get; set; }
        
        public string Owner { get; set; }
        public virtual List<string> Comments { get; set; }
    }
}