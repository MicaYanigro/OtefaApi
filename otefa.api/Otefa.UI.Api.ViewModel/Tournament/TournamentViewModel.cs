using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class TournamentViewModel
    {
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        public int TournamentFormat { get; set; }
        public int ClasificationFormat { get; set; }
        public string Rules { get; set; }
        public string Prices { get; set; }
        public List<int> Headquarters { get; set; }
        public List<DateTime> Dates { get; set; }
        public Dictionary<int, List<int>> TeamsPlayers { get; set; }
    }
}