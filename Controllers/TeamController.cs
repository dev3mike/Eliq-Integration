using System.Collections.Generic;
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
    public class TeamController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public TeamController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> listOfTeams()
        {
            return await context.Teams
            .ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Team>> getASingleTeam(int Id)
        {
            return await context.Teams
            .Include(s => s.Players)
            .FirstOrDefaultAsync(w => w.Id == Id);
        }

        [HttpPost]
        public async Task<ActionResult<TeamInsertDTO>> insert(TeamInsertDTO teamInsertDTO)
        {
            // Check Existance
            if (await isTeamExist(teamInsertDTO.Name)) return BadRequest("Team Name Exists");

            // Map to Team Entity
            var team = mapper.Map<Team>(teamInsertDTO);

            // Save to Database
            context.Teams.Add(team);
            await context.SaveChangesAsync();

            return Ok(teamInsertDTO);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TeamInsertDTO>> update(int Id, TeamInsertDTO teamInsertDTO)
        {
            var team = await context.Teams.FindAsync(Id);
            if (team == null) return BadRequest("Team Does Not Exist");

            team.Name = teamInsertDTO.Name;
            await context.SaveChangesAsync();

            return Ok(teamInsertDTO);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<TeamInsertDTO>> delete(int Id)
        {
            var team = await context.Teams.FindAsync(Id);
            if (team == null) return BadRequest("Team Does Not Exist");

            context.Teams.Remove(team);
            await context.SaveChangesAsync();

            return Ok();
        }


        private async Task<bool> isTeamExist(string name)
        {
            return await context.Teams.AnyAsync(x => x.Name == name);
        }
    }
}