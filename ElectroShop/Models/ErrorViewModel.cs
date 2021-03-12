using System;

namespace ElectroShop.Models
{
    /// <summary>
    /// This is a standard Error model that rerout os to a request.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
