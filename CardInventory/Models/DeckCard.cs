using System.ComponentModel.DataAnnotations;

namespace CardInventory.Models
{
    public class DeckCard
    {
	   [Required]
	   [Key]
	   public int Id { get; set; }

	   [Required]
	   public int DeckId { get; set; }

	   [Required]
	   public virtual Deck Deck { get; set; }

	   [Required]
	   public int CardId { get; set; }

	   [Required]
	   public virtual Card Card { get; set; }

	   [Required]
	   public int Quantity { get; set; }
    }
}