using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.ViewModels
{
    public class EditDeckViewModel
    { 
	   public int DeckId { get; set; }

	   [Required]
	   public string DeckName { get; set; }
	   public string UserId { get; set; }

    }
}
