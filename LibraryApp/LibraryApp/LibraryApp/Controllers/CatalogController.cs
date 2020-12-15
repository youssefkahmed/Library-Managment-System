using LibraryApp.Models.Catalog;
using LibraryApp.Models.CheckoutModels;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        private ICheckout _checkouts;

        public CatalogController(ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }

        public IActionResult Index() 
        {
            var assetModels = _assets.GetAll();

            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    Title = result.Title,
                    Type = _assets.GetType(result.Id),
                    AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                    ImageUrl = result.ImageUrl,
                    NumberOfCopies = result.NumberOfCopies.ToString()
                });

            var model = new AssetIndexModel { Assets = listingResult };

            return View(model);
        }

        public IActionResult Detail(int id) 
        {
            var asset = _assets.GetById(id);

            var currentHolds = _checkouts.GetCurrentHolds(id)
                               .Select(h => new AssetHoldModel 
                               {
                                   HoldPlaced = _checkouts.GetCurrentHoldPlaced(h.Id),
                                   PatronName = _checkouts.GetCurrentHoldPatronName(h.Id)
                               });

            var model = new AssetDetailModel
            {
                AssetId = id,
                AuthorOrDirector = _assets.GetAuthorOrDirector(id),
                Title = asset.Title,
                Type = _assets.GetType(id),
                Year = asset.Year,
                ISBN = _assets.GetIsbn(id),
                ImageUrl = asset.ImageUrl,
                Cost = asset.Cost,
                CurrentLocation = asset.Location.Name,
                Status = asset.Status.Name,
                DeweyCallNumber = _assets.GetDeweyIndex(id),
                CheckoutHistory = _checkouts.GetCheckoutHistory(id),
                CurrentHolds = currentHolds,
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                PatronName = _checkouts.GetCurrentCheckoutPatron(id)
            };

            return View(model);
        }

        public IActionResult Checkout(int id) 
        {
            var asset = _assets.GetById(id);

            var model = new CheckoutModel
            {
                LibraryCardId = "",
                AssetId = id,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkouts.CheckInItem(id);
            return RedirectToAction("Detail", new { id = id});
        }
        public IActionResult Hold(int id)
        {
            var asset = _assets.GetById(id);

            var model = new CheckoutModel
            {
                LibraryCardId = "",
                AssetId = id,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                HoldCount = _checkouts.GetCurrentHolds(id).Count()
            };

            return View(model);
        }

        public IActionResult MarkLost(int assetId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkouts.CheckOutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }
        
        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkouts.PlaceHold(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

    }
}
