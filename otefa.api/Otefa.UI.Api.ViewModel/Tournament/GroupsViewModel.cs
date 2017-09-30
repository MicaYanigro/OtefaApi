using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class GroupsViewModel
    {
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        public List<int> TeamsID { get; set; }

    }
}