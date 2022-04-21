using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CardInventory.Models;

namespace CardInventory.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any Cards.
            if (context.Cards.Any())
            {
                return;   // DB has been seeded
            }

            var cards = new Card[]
            {
                new Card { Name = "Bulbasaur", Type = "Pokemon", Quantity = 4 },
                new Card { Name = "Charmander", Type = "Pokemon", Quantity = 2 },
                new Card { Name = "Squirtle", Type = "Pokemon", Quantity = 4 },
                new Card { Name = "Pikachu", Type = "Pokemon", Quantity = 4 },
                new Card { Name = "Boss' Orders", Type = "Trainer", Quantity = 4 },
                new Card { Name = "Ultra Ball", Type = "Item", Quantity = 3 },
                new Card { Name = "Great Ball", Type = "Item", Quantity = 1 },
                new Card { Name = "Gengar", Type = "Pokemon", Quantity = 1 }
            };

            foreach (Card c in cards)
            {
                context.Cards.Add(c);
            }
            context.SaveChanges();

            if (context.Decks.Any())
            {
                return;   // DB has been seeded
            }

            var decks = new Deck[]
            {
                new Deck { Name = "Fire Deck", UserId = "John" },
                new Deck { Name = "Shadow Deck", UserId = "Scott" },
                new Deck { Name = "Water Deck", UserId = "Kevin" },
                new Deck { Name = "Dragon Deck", UserId = "Apple" },
                new Deck { Name = "Grass Deck", UserId = "Scott" },
                new Deck { Name = "Steel Deck", UserId = "Kevin" },
                new Deck { Name = "Psychic Deck", UserId = "Alex" },
                new Deck { Name = "Fighting Deck", UserId = "Joe" }
            };

            foreach (Deck d in decks)
            {
                context.Decks.Add(d);
            }
            context.SaveChanges();
        }
    }
}