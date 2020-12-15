using LibraryData.Models;
using System.Collections.Generic;

namespace LibraryData
{
    public interface IPatron
    {
        void Add(Patron newPatron);

        Patron Get(int id);
        IEnumerable<Patron> GetAll();
        IEnumerable<Checkout> GetCheckouts(int patronId);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId);
        IEnumerable<Hold> GetHolds(int patronId);
    }
}
