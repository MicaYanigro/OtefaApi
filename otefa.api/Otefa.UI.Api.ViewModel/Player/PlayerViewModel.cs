using System;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.Player
{
    public class PlayerViewModel
    {
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string Lastname { get; set; }
        [StringLength(8, MinimumLength = 1)]
        public string Dni { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string MedicalInsurance { get; set; }
        public DateTime BirthDate { get; set; }
        public string CelNumber { get; set; }
    }
}