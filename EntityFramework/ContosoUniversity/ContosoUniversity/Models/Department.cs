using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Required(ErrorMessage = "Budget is required.")]
        [Column(TypeName = "money")]
        [Range(0, 10000000, ErrorMessage="Budget must be between 0 and 10 million.")]
        public decimal? Budget { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Administrator")]
        public int? PersonId { get; set; }

        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}