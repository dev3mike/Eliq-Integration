using System;
using System.Collections.Generic;

namespace Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<Game> Games { get; set; }
        public ICollection<Player> Players { get; set; }

    }
}