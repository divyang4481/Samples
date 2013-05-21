using System.ComponentModel.DataAnnotations;

namespace OSIM.Core.Entities
{
    public class ItemType
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
