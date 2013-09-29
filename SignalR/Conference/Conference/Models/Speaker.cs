using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conference.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Speaker")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        // virtual - foreign key, lazy loading
        //public virtual List<Session> Sessions { get; set; }
    }
}
