using System.Collections.ObjectModel;

namespace Csharp_Advanced.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
    }

}
