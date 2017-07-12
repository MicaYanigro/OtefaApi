using System;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Team
{
    public class PutHeadquarterViewModel
    {

        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string address { get; set; }
        public string City { get; set; }
       


    }
}
