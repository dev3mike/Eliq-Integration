using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.I
{
    public class GameInsertDTO
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int GuestTeamId { get; set; }
        [Required]
        public DateTime GameDate { get; set; }
    }
}