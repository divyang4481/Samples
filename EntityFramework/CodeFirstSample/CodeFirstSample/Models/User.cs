using System.ComponentModel.DataAnnotations;

namespace CodeFirstSample.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}