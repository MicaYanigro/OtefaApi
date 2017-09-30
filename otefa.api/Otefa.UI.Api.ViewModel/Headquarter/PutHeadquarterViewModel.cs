using System;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Headquarter
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
