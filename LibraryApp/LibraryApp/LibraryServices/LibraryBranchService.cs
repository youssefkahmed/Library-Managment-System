using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
    public class LibraryBranchService : ILibraryBranch
    {
        private LibraryContext _context;

        public LibraryBranchService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryBranch newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int branchId)
        {
            return GetAll()
                   .FirstOrDefault(lb => lb.Id == branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                   .Include(lb => lb.Patrons)
                   .Include(lb => lb.LibraryAssets);
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.LibraryBranches
                   .Include(lb => lb.LibraryAssets)
                   .FirstOrDefault(lb => lb.Id == branchId)
                   .LibraryAssets;
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours
                        .Where(bh => bh.Id == branchId);
            return DataHelpers.HumanizeBusinessHours(hours);
        }

        public IEnumerable<Patron> GetPatrons(int branchId)
        {
            return _context.LibraryBranches
                    .Include(b => b.Patrons)
                    .FirstOrDefault(b => b.Id == branchId)
                    .Patrons;
        }

        public bool IsBranchOpen(int branchId)
        {
            var currentHour = DateTime.Now.Hour;
            var currentDay = (int)DateTime.Now.DayOfWeek + 1;
            var hours = _context.BranchHours
                        .Where(bh => bh.Id == branchId);
            var daysHours = hours.FirstOrDefault(h => h.DayOfWeek == currentDay);

            return currentHour < daysHours?.CloseTime && currentHour > daysHours?.OpenTime;

        }
    }
}
