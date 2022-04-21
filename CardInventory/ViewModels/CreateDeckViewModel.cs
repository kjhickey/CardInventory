using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.ViewModels
{
    public class CreateDeckViewModel
    {
	   [Required]
	   [Display(Name = "Deck Name")]
	   public string DeckName { get; set; }
    }
}
