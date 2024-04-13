using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace Csharp_Advanced.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public LocationType Type { get; set; }
        public int Rooms { get; set; }
        public int NumberOfGuests { get; set; }
        public Features AvailableFeatures { get; set; }
        public ObservableCollection<Image> Images { get; set; }
        public float PricePerDay { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Landlord Landlord { get; set; }
        [Flags]
        public enum Features
        {
            Smoking = 1,
            PetsAllowed = 2,
            Wifi = 4,
            TV = 8,
            Bath = 16,
            Breakfast = 32
        }

        public enum LocationType
        {
            Appartment,
            Cottage,
            Chalet,
            Room,
            Hotel,
            House
        }
    }

}
