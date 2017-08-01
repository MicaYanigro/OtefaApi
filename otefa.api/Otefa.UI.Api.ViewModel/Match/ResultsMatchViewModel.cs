using Otefa.UI.Api.ViewModel.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class ResultsMatchViewModel
    {

        public int MatchTeamID { get; set; }

        public int Goals { get; set; }

        public bool HasBonusPoint { get; set; }

        public int FigureID { get; set; }
        
        public List<PlayersDetailsViewModel> PlayersDetails { get; set; }

    }
}