using CardInventory.Data;
using CardInventory.Models;
using CardInventory.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardInventory.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
	   private readonly ApplicationDbContext _db;
	   private readonly UserManager<ApplicationUser> _userManager;

	   public CardController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
	   {
		  _db = db;
		  _userManager = userManager;
	   }
	   public IActionResult Index()
	   {
		  IEnumerable<Card> cardList = _db.Cards;

		  return View(cardList);
	   }

	   public IActionResult Details(int cardId)
	   {
		  CardPageViewModel vm = new();

		  Card matchingCard = _db.Cards.Find(cardId);
		  vm.Card = matchingCard;

		  var userId = _userManager.GetUserId(User);

		  vm.RelatedDecks = _db.DeckCards
			 .Where(deckCard => deckCard.Deck.UserId == userId)
			 .Where(deckCard => deckCard.CardId == cardId)
			 .Select(deckCard => deckCard.Deck)
			 .ToList();

		  return View(vm);
	   }

	   public IActionResult Create()
	   {
		  return View();
	   }

	   // POST-Create
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Create(Card card)
	   {
		  try
		  {
			 Card newCard = new Card();
			 newCard.UserId = _userManager.GetUserId(User);
			 newCard.Name = card.Name;
			 newCard.Type = card.Type;
			 newCard.Quantity = card.Quantity;

			 if (ModelState.IsValid)
			 {
				_db.Add(newCard);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			 }

		  }
		  catch (Exception e)
		  {
			 ModelState.AddModelError("", e.Message);
		  }
		  return View(card);
	   }

	   // GET: Card/Edit
	   public async Task<IActionResult> Edit(int id)
	   {
		  var cardToUpdate = await _db.Cards.FirstOrDefaultAsync(x => x.Id == id);
		  return View(cardToUpdate);
	   }

	   // POST: Card/Edit/
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Edit(Card card)
	   {

		  try
		  {

			 if (ModelState.IsValid)
			 {
				_db.Update(card);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			 }

		  }
		  catch (Exception e)
		  {
			 ModelState.AddModelError("", e.Message);
		  }
		  return View(card);
	   }

	   // GET: Card/Delete
	   public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var card = await _db.Cards
			 .AsNoTracking()
			 .FirstOrDefaultAsync(x => x.Id == id);
		  if (card == null)
		  {
			 return NotFound();
		  }

		  if (saveChangesError.GetValueOrDefault())
		  {
			 ViewData["ErrorMessage"] =
				"Delete failed. Try again, and if the problem persists " +
				"see your system administrator.";
		  }

		  return View(card);
	   }

	   // POST: Card/Delete
	   [HttpPost, ActionName("Delete")]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> DeleteConfirmed(int id)
	   {
		  var card = await _db.Cards.FindAsync(id);
		  if (card == null)
		  {
			 return RedirectToAction(nameof(Index));
		  }

		  try
		  {
			 _db.Cards.Remove(card);
			 await _db.SaveChangesAsync();
			 return RedirectToAction(nameof(Index));
		  }
		  catch (DbUpdateException /* ex */)
		  {
			 //Log the error (uncomment ex variable name and write a log.)
			 return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
		  }
	   }

	   [HttpGet]
	   public JsonResult SearchCards(string input)
	   {
		  List<Card> result = _db.Cards.Where(card => card.Name.ToLower().Contains(input.ToLower())).Take(4).ToList();

		  return Json(result);
	   }
    }
}
