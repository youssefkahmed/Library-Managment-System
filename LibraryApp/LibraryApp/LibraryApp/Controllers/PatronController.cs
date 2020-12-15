using LibraryApp.Models.Patron;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Controllers
{
    public class PatronController : Controller
    {
        private IPatron _patron;

        public PatronController(IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index() 
        {
            var allPatrons = _patron.GetAll();

            var patronModels = allPatrons.Select(patron => new PatronDetailModel
            {
                Id = patron.Id,
                LibraryCardId = patron.LibraryCard.Id,
                FirstName = patron.FirstName,
                LastName = patron.LastName,
                TelephoneNumber = patron.TelephoneNumber,
                Address = patron.Address,
                OverdueFees = patron.LibraryCard.Fees,
                HomeLibraryBranch = patron.LibraryBranch.Name,
                AssetsCheckedOut = _patron.GetCheckouts(patron.Id),
                CheckoutHistory = _patron.GetCheckoutHistory(patron.Id),
                Holds = _patron.GetHolds(patron.Id),
                MemberSince = patron.LibraryCard.Created
            }).ToList();

            var model = new PatronIndexModel
            {
                Patrons = patronModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var patron = _patron.Get(id);

            var model = new PatronDetailModel
            {
                Id = patron.Id,
                LibraryCardId = patron.LibraryCard.Id,
                FirstName = patron.FirstName,
                LastName = patron.LastName,
                TelephoneNumber = patron.TelephoneNumber,
                Address = patron.Address,
                OverdueFees = patron.LibraryCard.Fees,
                HomeLibraryBranch = patron.LibraryBranch.Name,
                AssetsCheckedOut = _patron.GetCheckouts(patron.Id) ?? new List<Checkout>(),
                CheckoutHistory = _patron.GetCheckoutHistory(patron.Id),
                Holds = _patron.GetHolds(patron.Id),
                MemberSince = patron.LibraryCard.Created
            };
            
            return View(model);
        }
    }
}
