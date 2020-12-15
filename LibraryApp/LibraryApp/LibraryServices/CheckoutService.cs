using LibraryData;
using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.InteropServices.ComTypes;

namespace LibraryServices
{
    public class CheckoutService : ICheckout
    {
        private LibraryContext _context;

        public CheckoutService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public void CheckInItem(int assetId)
        {
            DateTime now = DateTime.Now;

            var item = _context.LibraryAssets
                       .FirstOrDefault(asset => asset.Id == assetId);

            RemoveExistingCheckouts(assetId);
            CloseExistingCheckoutHistory(assetId, now);

            var currentHolds = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == assetId);

            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(assetId, currentHolds);
                return;
            }

            UpdateAssetStatus(assetId, "Available");

            _context.SaveChanges(); 
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
            }

            DateTime now = DateTime.Now;
            var item = _context.LibraryAssets
                       .FirstOrDefault(asset => asset.Id == assetId);
            UpdateAssetStatus(assetId, "Checked Out");

            var libraryCard = _context.LibraryCards
                              .FirstOrDefault(card => card.Id == libraryCardId);

            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };
            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                CheckedOut = now
            };
            _context.Add(checkoutHistory);

            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            return _context.CheckoutHistories
                .Include(ch => ch.LibraryAsset)
                .Include(ch => ch.LibraryCard)
                .Where(ch => ch.LibraryAsset.Id == id);
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            var hold = _context.Holds
                       .Include(h => h.LibraryAsset)
                       .Include(h => h.LibraryCard)
                       .FirstOrDefault(h => h.Id == holdId);

            var cardId = hold?.LibraryCard.Id;

            var patron = _context.Patrons
                         .Include(p => p.LibraryCard)
                         .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron?.FirstName + " " + patron?.LastName;
        }

        public DateTime GetCurrentHoldPlaced(int holdId)
        {
            return _context.Holds
                   .FirstOrDefault(h => h.Id == holdId)
                   .HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Holds
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return _context.Checkouts
                .Include(c => c.LibraryAsset)
                .Where(c => c.LibraryAsset.Id == assetId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, "Lost");
            _context.SaveChanges();
        }

        public void MarkFound(int assetId)
        {
            DateTime now = DateTime.Now;

            UpdateAssetStatus(assetId, "Available");
            RemoveExistingCheckouts(assetId);
            CloseExistingCheckoutHistory(assetId, now);

            _context.SaveChanges();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            DateTime now = DateTime.Now;

            var item = _context.LibraryAssets
                       .Include(asset => asset.Status)
                       .FirstOrDefault(asset => asset.Id == assetId);

            var card = _context.LibraryCards
                       .FirstOrDefault(card => card.Id == libraryCardId);

            if (item.Status.Name == "Available")
                UpdateAssetStatus(assetId, "On Hold");
            
            var hold = new Hold
            {
                LibraryAsset = item,
                LibraryCard = card,
                HoldPlaced = now
            };

            _context.Add(hold);
            _context.SaveChanges();
        }
        
        public string GetCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);

            if (checkout == null)
            {
                return "";
            }

            var cardId = checkout.LibraryCard.Id;

            var patron = _context.Patrons
                         .Include(p => p.LibraryCard)
                         .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron.FirstName + " " + patron.LastName;
        }
        
        public bool IsCheckedOut(int assetId)
        {
            return _context.Checkouts
                   .Where(c => c.LibraryAsset.Id == assetId).Any();
        }

        // Auxiliary Functions
        private void UpdateAssetStatus(int assetId, string status)
        {
            var item = _context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == assetId);

            _context.Update(item);
            item.Status = _context.Statuses.FirstOrDefault(s => s.Name == status);
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            var checkout = _context.Checkouts
                           .FirstOrDefault(c => c.LibraryAsset.Id == assetId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        private void CloseExistingCheckoutHistory(int assetId, DateTime now)
        {
            var history = _context.CheckoutHistories
                          .FirstOrDefault(ch => ch.LibraryAsset.Id == assetId);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
        }

        private void CheckoutToEarliestHold(int assetId, IQueryable<Hold> currentHolds)
        {
            var earliestHold = currentHolds.OrderBy(h => h.HoldPlaced).FirstOrDefault();
            var libraryCard = earliestHold.LibraryCard;

            _context.Remove(earliestHold);
            _context.SaveChanges();
            CheckOutItem(assetId, libraryCard.Id);
        }
        

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }


        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _context.Checkouts
                    .Include(c => c.LibraryAsset)
                    .Include(c => c.LibraryCard)
                    .FirstOrDefault(c => c.LibraryAsset.Id == assetId);
        }
    }
}
