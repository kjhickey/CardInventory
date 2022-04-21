using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.Models
{
    public class Deck
    {
	   [Required]
	   [Key]
	   public int Id { get; set; }

	   [Required]
	   public string Name { get; set; }

	   [Required]
	   [Display(Name = "User")]
	   public string UserId { get; set; }

	   public virtual ApplicationUser User { get; set; }

	   public virtual List<DeckCard> DeckCards { get; set;}

    }
}
