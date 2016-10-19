using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refwebportal.Models
{
    public class GamesIndexData
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable <GameTeam> GameTeams { get; set; }
        public IEnumerable<GamePlayer> GamePlayers { get; set; }
    }
}