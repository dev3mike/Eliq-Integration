using System.ComponentModel.DataAnnotations;

namespace DTO.I
{
    public class PlayerInsertDTO
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Position { get; set; }
        public bool IsAReservePlayer { get; set; } = false;
    }
}