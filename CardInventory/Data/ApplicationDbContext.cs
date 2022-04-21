using CardInventory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardInventory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
	   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		  : base(options)
	   {
	   }

	   public DbSet<Card> Cards { get; set; }

	   public DbSet<Deck> Decks { get; set; }

	   public DbSet<DeckCard> DeckCards { get; set; }
    }
}
