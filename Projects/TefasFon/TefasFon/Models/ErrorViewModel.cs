using System.Collections.Generic;

namespace TefasFon.Models
{

    public class DataL
    {
        // Verileri taşımak için liste
        public List<string>? DataList { get; set; }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }


}