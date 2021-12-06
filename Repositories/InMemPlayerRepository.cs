
using System;
using System.Collections.Generic;
using GameInventory.Entities;

namespace GameInventoryAPI.Repositories 
{
    public class InMemPlayerRepository 
    {
        private readonly List<Player> players = new()
        {
            new Player{ Id=Guid.NewGuid(),Name="Oberyn Martell",Level=69,PrimeStats = new(){
             Agility = 69,
             Dexterity = 55,
             Intelligence = 32,
             Strength = 46
            }}
        };

        public IEnumerable<Player> GetPlayers()
        {
            foreach(Player thisplayer in players)
            {
                thisplayer.SecStats = thisplayer.GetSecStats();
            }
            return players;
        }
    }
}