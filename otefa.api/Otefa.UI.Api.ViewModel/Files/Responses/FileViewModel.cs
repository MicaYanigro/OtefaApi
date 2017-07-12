using System;

namespace Otefa.UI.Api.ViewModel.Files.Responses
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
    }
}