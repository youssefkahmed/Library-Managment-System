using LibraryApp.Models.Branch;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryApp.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branch;

        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }

        public IActionResult Index() 
        {
            var branchModels = _branch.GetAll().Select(b => new BranchDetailModel 
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                TelephoneNumber = b.Telephone,
                OpenDate = b.OpenDate.ToString(),
                IsOpen = _branch.IsBranchOpen(b.Id),
                OpenHours = _branch.GetBranchHours(b.Id),
                NumberOfAssets = _branch.GetAssets(b.Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(b.Id).Count(),
                TotalAssetValue = _branch.GetAssets(b.Id).Select(a => a.Cost).Sum()
            }).ToList();

            var model = new BranchIndexModel { Branches = branchModels };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branch.Get(id);

            var model = new BranchDetailModel
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Description = branch.Description,
                ImageUrl = branch.ImageUrl,
                TelephoneNumber = branch.Telephone,
                OpenDate = branch.OpenDate.ToString(),
                IsOpen = _branch.IsBranchOpen(branch.Id),
                OpenHours = _branch.GetBranchHours(branch.Id),
                NumberOfAssets = _branch.GetAssets(branch.Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(branch.Id).Count(),
                TotalAssetValue = _branch.GetAssets(branch.Id).Select(a => a.Cost).Sum()
            };

            return View(model);
        }
    }
}
