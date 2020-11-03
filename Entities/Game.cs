using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities
{
    [Table("Games")]
    public class Game
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public int TeamScore { get; set; }
        public Team GuestTeam { get; set; }
        public int GuestTeamId { get; set; }
        public int GuestTeamScore { get; set; }
        public DateTime GameDate { get; set; } = DateTime.Now;
    }
}