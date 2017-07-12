using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class MatchViewModel
    {

        public int Headquarter { get; set; }

        public DateTime Date { get; set; }

        public List<int> Teams { get; set; }

    }
}