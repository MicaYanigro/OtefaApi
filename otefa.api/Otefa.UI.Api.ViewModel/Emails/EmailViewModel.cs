using System.Collections.Generic;

namespace Otefa.UI.Api.ViewModel.Emails
{
    public class EmailViewModel
    {
        public string Body { get; set; }

        public List<string> ReplyTo { get; set; }
    }
}