using CardInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.ViewModels
{
    public class CardPageViewModel
    {
	   public Card Card { get; set; }

	   public List<Deck> RelatedDecks { get; set; }
    }
}
