using System;

namespace CardInventory.Models
{
    public class ErrorViewModel
    {
	   public string RequestId { get; set; }

	   public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
