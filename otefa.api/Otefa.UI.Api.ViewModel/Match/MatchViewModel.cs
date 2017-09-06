using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class MatchViewModel
    {
        public int Tournament { get; set; }

        public int Headquarter { get; set; }

        public DateTime Date { get; set; }

        public int Round { get; set; }

        public List<int> Teams { get; set; }

    }
}