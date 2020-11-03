using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Players")]
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public bool IsAReservePlayer { get; set; } = false;
    }
}