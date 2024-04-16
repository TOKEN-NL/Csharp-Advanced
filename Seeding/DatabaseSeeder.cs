using System;
using System.Collections.ObjectModel;
using System.Linq;
using Csharp_Advanced.Models;
using static System.Net.WebRequestMethods;
using static Csharp_Advanced.Models.Location;

namespace Csharp_Advanced.Seeding
{
    public static class DatabaseSeeder
    {
        public static void DeleteAllEntries(AppDbContext dbContext)
        {
            // Retrieve all entries from the database
            var allLocations = dbContext.Locations.ToList();
            var allCustomers = dbContext.Customers.ToList();
            var allImages = dbContext.Images.ToList();
            var allLandlords = dbContext.Landlords.ToList();
            // Add additional DbSet properties here if needed

            // Remove each entry
            dbContext.Images.RemoveRange(allImages);
            dbContext.Landlords.RemoveRange(allLandlords);
            dbContext.Locations.RemoveRange(allLocations);
            dbContext.Customers.RemoveRange(allCustomers);
            // Add additional RemoveRange calls for other DbSet properties if needed

            // Save changes
            dbContext.SaveChanges();
        }
        public static void Seed(AppDbContext dbContext)
        {
           
            if (dbContext.Locations.Any())
            {
                
                return;
            }

            var landlord = new Landlord
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 40,
                Avatar =  new Image
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/37/The_owner_of_a_old_book_selling_store_at_Golpark%2C_South_Kolkata.jpg/800px-The_owner_of_a_old_book_selling_store_at_Golpark%2C_South_Kolkata.jpg?20221103113259"
                }
        };
            dbContext.Images.Add(landlord.Avatar);
            dbContext.Landlords.Add(landlord);
            dbContext.SaveChanges();

            
            var location1 = new Location
            {
                Title = "De Boerenhoeve",
                SubTitle = "Lekker veel ruimte",
                Description = "De camping ligt verscholen achter de boerderij in de polder. Op fietsafstand (5 minuten) liggen het dorpje Nieuwvliet, de zee, het strand, het bos van Erasmus en het natuurgebied de Knokkert.",
                Rooms = 5,
                NumberOfGuests = 12,
                PricePerDay = 300,
                Type = Location.LocationType.Cottage,
                Landlord = landlord, 
                AvailableFeatures = (Features)55
               
            };
            var imagesLocation1 = new ObservableCollection<Image>
            {
                new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/Living_house_at_Mbuga.jpg/450px-Living_house_at_Mbuga.jpg?20220330083910", IsCover = true },
                new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Living_house_at_Gihosha.jpg/450px-Living_house_at_Gihosha.jpg?20220420052614" },
                new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Living_house_in_Kigobe.jpg/450px-Living_house_in_Kigobe.jpg?20220420064942" }
            };
            location1.Images = imagesLocation1;

            dbContext.Locations.Add(location1);

            var location2 = new Location
            {
                Title = "Frankie's Penthouse",
                SubTitle = "Te gek uitzicht",
                Description = "Nee, dit puike penthouse dat al jaren te koop stond en nu is verkocht, is niet de duurste woning van ons land. Bij lange na niet. Wel is de meer dan €30.000 per vierkante meter woonruimte een record in ons land.",
                Rooms = 2,
                NumberOfGuests = 4,
                PricePerDay = 400,
                Type = Location.LocationType.Appartment,
                Landlord = landlord,
                AvailableFeatures = (Features)35
            };
            var imagesLocation2 = new ObservableCollection<Image>
            {
                new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Student_Accommodation_-_geograph.org.uk_-_5408823.jpg/800px-Student_Accommodation_-_geograph.org.uk_-_5408823.jpg?20230908024540", IsCover = true },
                new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Student_accommodation%2C_Lanfranc_House_-_geograph.org.uk_-_3641903.jpg/800px-Student_accommodation%2C_Lanfranc_House_-_geograph.org.uk_-_3641903.jpg?20220716144048" }
            };

            location2.Images = imagesLocation2;

            dbContext.Locations.Add(location2);
            dbContext.SaveChanges();
        }
    }
}

