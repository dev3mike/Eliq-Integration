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
    public class PlayerController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public PlayerController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GameInsertDTO>> insert(PlayerInsertDTO playerInsertDTO)
        {
            // Find Host Team
            var team = await context.Teams.FindAsync(playerInsertDTO.TeamId);

            if (team == null) return BadRequest("Team Not Found");

            // Map Object to Entity
            var player = mapper.Map<Player>(playerInsertDTO);

            if (team.Players == null)
            {
                team.Players = new Collection<Player>();
            }
            team.Players.Add(player);
            context.Entry(team).State = EntityState.Modified;

            // Save to Database
            await context.SaveChangesAsync();

            return Ok(playerInsertDTO);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> delete(int Id)
        {
            var player = await context.Players.FindAsync(Id);
            if (player == null) return BadRequest("Player Does Not Exist");

            context.Players.Remove(player);
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}