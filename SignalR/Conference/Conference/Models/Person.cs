using System.ComponentModel.DataAnnotations;

namespace Conference.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MinLength(3)]
        public string Name { get; set; }

        [Range(0, 400)]
        public int Height { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}