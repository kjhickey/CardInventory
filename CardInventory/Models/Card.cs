using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.Models
{
    public class Card
    {
	   [Required]
	   [Key]
	   public int Id { get; set; }

	   [Required]
	   [MaxLength(255)]
	   [Display(Name = "Name")]
	   public string Name { get; set; }

	   [Required]
	   [MaxLength(255)]
	   public string Type { get; set; }

	   [Required]
	   public int Quantity { get; set; }

	   public byte[] Image { get; set; }

	   public string ImageUrl { get; set; }

	   public string UserId { get; set; }

	   public virtual ApplicationUser User { get; set; }

    }
}
