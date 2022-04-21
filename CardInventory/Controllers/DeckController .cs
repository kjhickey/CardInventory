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
    public class DeckController : Controller
    {
	   private readonly ApplicationDbContext _db;
	   private readonly UserManager<ApplicationUser> _userManager;

	   public DeckController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
	   {
		  _db = db;
		  _userManager = userManager;
	   }
	   public IActionResult Index()
	   {
		  IEnumerable<Deck> deckList = _db.Decks;
		  return View(deckList);
	   }

	   // GET-Create
	   public IActionResult Create()
	   {
		  return View();
	   }

	   // POST-Create
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Create(CreateDeckViewModel deckVM)
	   {
		  try
		  {
			 Deck newDeck = new Deck();
			 newDeck.UserId = _userManager.GetUserId(User);
			 newDeck.Name = deckVM.DeckName;

			 if (ModelState.IsValid)
			 {
				_db.Add(newDeck);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			 }

		  }
		  catch (Exception e)
		  {
			 ModelState.AddModelError("", e.Message);
		  }
		  return View(deckVM);
	   }

	   // GET: Deck/Edit
	   public async Task<IActionResult> Edit(int id)
	   {
		  var deckToUpdate = await _db.Decks.FirstOrDefaultAsync(deck => deck.Id == id);
		  return View(deckToUpdate);
	   }

	   // POST: Decks/Edit/
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Edit(Deck deck)
	   {

		  try
		  {

			 if (ModelState.IsValid)
			 {
				_db.Update(deck);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			 }

		  }
		  catch (Exception e)
		  {
			 ModelState.AddModelError("", e.Message);
		  }
		  return View(deck);
	   }

	   public async Task<IActionResult> Details(int deckId)
	   {
		  var deck = await _db.Decks.Include("DeckCards.Card").SingleOrDefaultAsync(deck => deck.Id == deckId);
		  if (deck == null)
		  {
			 return NotFound();
		  }
		  return View(deck);
	   }

	   [HttpPost]
	   public IActionResult AddCards(int deckId, int cardId, int quantity)
	   {
		  DeckCard newDeckCard = new()
		  {
			 DeckId = deckId,
			 CardId = cardId,
			 Quantity = quantity
		  };

		  _db.DeckCards.Add(newDeckCard);
		  _db.SaveChanges();

		  return RedirectToAction(nameof(Details), new { deckId });
	   }
    }
}
