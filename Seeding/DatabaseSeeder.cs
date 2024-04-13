using System;
using System.Linq;
using Csharp_Advanced.Models;
using static Csharp_Advanced.Models.Location;

namespace Csharp_Advanced.Seeding
{
    public static class DatabaseSeeder
    {
        public static void Seed(AppDbContext dbContext)
        {
            // Controleer of er al gegevens in de database staan
            if (dbContext.Locations.Any())
            {
                return; // Database is al geïnitialiseerd
            }

            // Voeg voorbeeldgegevens toe aan de database
            var image = new Image
            {
                Url = "test"
            };
            dbContext.Images.Add(image);
            dbContext.SaveChanges();

            var landlord = new Landlord
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 40,
                Avatar = image
            };

            dbContext.Landlords.Add(landlord);
            dbContext.SaveChanges();

            // Voeg voorbeeldgegevens toe aan de database en koppel deze aan de Landlord
            var location1 = new Location
            {
                Title = "De Boerenhoeve",
                SubTitle = "Lekker veel ruimte",
                Description = "De camping ligt verscholen achter de boerderij in de polder. Op fietsafstand (5 minuten) liggen het dorpje Nieuwvliet, de zee, het strand, het bos van Erasmus en het natuurgebied de Knokkert.",
                Rooms = 5,
                NumberOfGuests = 12,
                PricePerDay = 300,
                Type = Location.LocationType.Cottage,
                Landlord = landlord // Koppel de locatie aan de Landlord
            };

            var location2 = new Location
            {
                Title = "Frankie's Penthouse",
                SubTitle = "Te gek uitzicht",
                Description = "Nee, dit puike penthouse dat al jaren te koop stond en nu is verkocht, is niet de duurste woning van ons land. Bij lange na niet. Wel is de meer dan €30.000 per vierkante meter woonruimte een record in ons land.",
                Rooms = 2,
                NumberOfGuests = 4,
                PricePerDay = 400,
                Type = Location.LocationType.Appartment,
                Landlord = landlord // Koppel de locatie aan dezelfde Landlord
            };

            dbContext.Locations.AddRange(location1, location2);
            dbContext.SaveChanges();
        }
    }
}

