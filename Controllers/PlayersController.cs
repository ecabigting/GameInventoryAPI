
using System.Collections.Generic;
using GameInventory.Entities;
using GameInventoryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameInventoryAPI.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly InMemPlayerRepository repo;

        public PlayersController()
        {
            repo = new InMemPlayerRepository();
        }

        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            var players = repo.GetPlayers();
            return players;
        }
    }
}