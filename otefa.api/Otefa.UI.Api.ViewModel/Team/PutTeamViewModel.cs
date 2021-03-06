﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class PutTeamViewModel
    {

        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string TeamDelegate { get; set; }
        public string ShieldImage { get; set; }
        public string TeamImage { get; set; }


    }
}
