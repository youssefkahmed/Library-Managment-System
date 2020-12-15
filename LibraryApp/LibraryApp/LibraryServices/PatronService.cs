using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
    public class PatronService : IPatron
    {
        private LibraryContext _context;

        public PatronService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Patron newPatron)
        {
            _context.Add(newPatron);
            _context.SaveChanges();
        }

        public Patron Get(int id)
        {
            return GetAll()
                .Where(patron => patron.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons
                    .Include(patron => patron.LibraryCard)
                    .Include(patron => patron.LibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.Id; 

            return _context.CheckoutHistories
                    .Include(ch => ch.LibraryCard)
                    .Include(ch => ch.LibraryAsset)
                    .Where(ch => ch.LibraryCard.Id == cardId)
                    .OrderByDescending(ch => ch.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.Id;

            return _context.Checkouts
                    .Include(c => c.LibraryCard)
                    .Include(c => c.LibraryAsset)
                    .Where(c => c.LibraryCard.Id == cardId);
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.Id;

            return _context.Holds
                    .Include(h => h.LibraryCard)
                    .Include(h => h.LibraryAsset)
                    .Where(h => h.LibraryCard.Id == cardId)
                    .OrderByDescending(h => h.HoldPlaced);
        }

    }
}
