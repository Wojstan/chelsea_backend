using System.Collections.Generic;
using System.Linq;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerController(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlayerEntity>> GetAll()
        {
            string[] positions = {"Goalkeepers", "Defenders", "Midfielders", "Strikers"};
            var players = _playerRepository.GetAll();

            var allPlayers = positions.Select((position, index) => new PlayerResponse
            {
                Name = position,
                Players = players
                    .Where(player => player.Position == index).ToList()
            });

            return Ok(allPlayers);
        }
    }
}