using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.DTOs
{
    public class DeckCardInfoDto
    {
	   public int DeckId { get; set; }

	   public int CardId { get; set; }

	   public int DeckQuantity { get; set; }

	   public int BinderTotal { get; set; }

	   public int TotalInUse { get; set; }
    }
}
