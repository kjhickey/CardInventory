using CardInventory.Data;
using CardInventory.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.Controllers
{
    public class DeckCardController : Controller
    {
	   private ApplicationDbContext _db;

	   public DeckCardController(ApplicationDbContext dbContext)
	   {
		  _db = dbContext;
	   }

	   // POST: DeckCard/Delete
	   [HttpPost, ActionName("Delete")]
	   public async Task<bool> Delete(int id)
	   {
		  var card = await _db.DeckCards.FindAsync(id);
		  if (card == null)
		  {
			 return false;
		  }

		  _db.DeckCards.Remove(card);
		  await _db.SaveChangesAsync();
		  return true;
		
	   }

	   [HttpGet]
	   public async Task<DeckCardInfoDto> GetDeckCardInfo(int deckId, int cardId)
	   {
		  DeckCardInfoDto infoDto = new();

		  var result = await _db.DeckCards.Include("Card").FirstOrDefaultAsync(x => x.DeckId == deckId && x.CardId == cardId);

		  infoDto.CardId = cardId;
		  infoDto.DeckId = deckId;

		  if(result != null)
		  {
			 infoDto.DeckQuantity = result.Quantity;
			 infoDto.BinderTotal = result.Card.Quantity;

			 // working on it
			 infoDto.TotalInUse = 0;
		  }


		  return infoDto;
	   }
    }
}
