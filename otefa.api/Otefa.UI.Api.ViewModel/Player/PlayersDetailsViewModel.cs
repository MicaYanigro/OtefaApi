using System;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Player
{
    public class PlayersDetailsViewModel
    {
        public int PlayerID { get; set; }
        public int? Goals { get; set; }
        public bool Played { get; set; }
        public int? Card { get; set; }
        public string Observation { get; set; }
    }
}