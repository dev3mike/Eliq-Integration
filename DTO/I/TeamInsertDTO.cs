using System.ComponentModel.DataAnnotations;

namespace DTO.I
{
    public class TeamInsertDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
    }
}