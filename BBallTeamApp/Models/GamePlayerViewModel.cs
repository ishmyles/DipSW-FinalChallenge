using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBallTeamApp.Models
{
    public class GamePlayerViewModel
    {
        public List<Player> Players { get; set; }
        public List<GameDay> GameDays { get; set; }
    }
}