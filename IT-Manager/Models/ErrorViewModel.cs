using System;

namespace itmanager.Models
{
    public class ErrorViewModel
    {
        // error model set up for error view 
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
