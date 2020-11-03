using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using AutoMapper;
using DTO;
using DTO.I;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    public class GameController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public GameController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{filter}")]
        public async Task<ActionResult<IEnumerable<Game>>> listOfGames(string filter)
        {
            var games = context.Games;
            // Filter Games [ upcoming , previous ]
            switch (filter)
            {
                case "upcoming":
                    games.Where(s => s.GameDate > DateTime.Now);
                    break;
                case "previous":
                    games.Where(s => s.GameDate <= DateTime.Now);
                    break;
            }

            return await games
            .Include(s => s.Team)
            .Include(s => s.GuestTeam)
            .AsNoTracking()
            .ToListAsync(); ;
        }

        [HttpPost]
        public async Task<ActionResult<GameInsertDTO>> insert(GameInsertDTO gameInsertDto)
        {
            // Find Host Team
            var team = await context.Teams.FindAsync(gameInsertDto.TeamId);
            var guestTeam = await context.Teams.FindAsync(gameInsertDto.GuestTeamId);

            if (team == null) return BadRequest("Host Team Not Found");
            if (guestTeam == null) return BadRequest("Guest Team Not Found");

            // Map Object to Entity
            var game = mapper.Map<Game>(gameInsertDto);

            if (team.Games == null)
            {
                team.Games = new Collection<Game>();
            }
            team.Games.Add(game);
            context.Entry(team).State = EntityState.Modified;

            // Save to Database
            await context.SaveChangesAsync();

            return Ok(gameInsertDto);
        }
    }
}