using System;
using System.ComponentModel.DataAnnotations;

namespace Otefa.UI.Api.ViewModel.New
{
    public class PutNewViewModel
    {

        public DateTime Date { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
    }
}
