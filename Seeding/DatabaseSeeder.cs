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
                new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Student_accommodation%2C_Lanfranc_House_-_geograph.org.uk_-_3641903.jpg/800px-Student_accommodation%2C_Lanfranc_House_-_geograph.org.uk_-_3641903.jpg?20220716144048" },
                new Image { Url = "https://commons.wikimedia.org/wiki/File:West_Yellowstone_(MT,_USA),_Targhee_Pass_Highway,_Haus_--_2022_--_2396.jpg" }

            };

            location2.Images = imagesLocation2;

            dbContext.Locations.Add(location2);
            var landlord2 = new Landlord
            {
                FirstName = "Emma",
                LastName = "Smith",
                Age = 35,
                Avatar = new Image
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Melisha_Ramessar.jpg/640px-Melisha_Ramessar.jpg"
                }
            };
            dbContext.Images.Add(landlord2.Avatar);
            dbContext.Landlords.Add(landlord2);
            dbContext.SaveChanges();


            var location3 = new Location
            {
                Title = "Luxe Villa aan het Strand",
                SubTitle = "Prachtig uitzicht op zee",
                Description = "Deze luxe villa ligt direct aan het strand en biedt een adembenemend uitzicht op de zee. Met 8 ruime kamers en een groot terras is het de perfecte plek voor een ontspannen vakantie.",
                Rooms = 8,
                NumberOfGuests = 16,
                PricePerDay = 1000,
                Type = Location.LocationType.House,
                Landlord = landlord2,
                AvailableFeatures = (Features)63
            };
            var imagesLocation3 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/M%C3%BCnster%2C_Lambertikirchplatz%2C_H%C3%A4user_--_2020_--_8297.jpg/800px-M%C3%BCnster%2C_Lambertikirchplatz%2C_H%C3%A4user_--_2020_--_8297.jpg?20200706115107", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/Terraced_Houses_on_Kings_Road_-_geograph.org.uk_-_1954920.jpg/640px-Terraced_Houses_on_Kings_Road_-_geograph.org.uk_-_1954920.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/Houses_on_the_east_side_of_Penton_Place%2C_Walworth_-_geograph.org.uk_-_4462037.jpg/640px-Houses_on_the_east_side_of_Penton_Place%2C_Walworth_-_geograph.org.uk_-_4462037.jpg" }
};
            location3.Images = imagesLocation3;

            dbContext.Locations.Add(location3);

            var location4 = new Location
            {
                Title = "Gezellig Chalet in de Bergen",
                SubTitle = "Ideaal voor natuurliefhebbers",
                Description = "Dit gezellige chalet is gelegen in de prachtige bergen en biedt een rustige en ontspannen omgeving. Met 4 comfortabele kamers en een knusse open haard is het perfect voor een uitje in de natuur.",
                Rooms = 4,
                NumberOfGuests = 8,
                PricePerDay = 600,
                Type = Location.LocationType.Chalet,
                Landlord = landlord2,
                AvailableFeatures = (Features)47
            };
            var imagesLocation4 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/Victorian_terraced_houses%2C_Lavender_Hill_-_geograph.org.uk_-_2412311.jpg/640px-Victorian_terraced_houses%2C_Lavender_Hill_-_geograph.org.uk_-_2412311.jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8a/Burgsteinfurt%2C_Haus_Loreto_--_2014_--_2422.jpg/640px-Burgsteinfurt%2C_Haus_Loreto_--_2014_--_2422.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7b/Terraced_houses_in_Rampart_Road_-_geograph.org.uk_-_3745930.jpg/640px-Terraced_houses_in_Rampart_Road_-_geograph.org.uk_-_3745930.jpg" }
};

            location4.Images = imagesLocation4;

            dbContext.Locations.Add(location4);

            var location5 = new Location
            {
                Title = "Stijlvol Appartement in het Centrum",
                SubTitle = "Levendige locatie in de stad",
                Description = "Dit stijlvolle appartement is gelegen in het bruisende centrum van de stad en biedt een moderne en comfortabele leefruimte. Met 2 slaapkamers en een balkon met uitzicht op de stad is het ideaal voor stadsbewoners.",
                Rooms = 2,
                NumberOfGuests = 4,
                PricePerDay = 800,
                Type = Location.LocationType.Appartment,
                Landlord = landlord2,
                AvailableFeatures = (Features)31
            };
            var imagesLocation5 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/db/Terraced_houses%2C_Bath_Road%2C_Banbury_-_geograph.org.uk_-_4407833.jpg/640px-Terraced_houses%2C_Bath_Road%2C_Banbury_-_geograph.org.uk_-_4407833.jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/House_8.jpg/640px-House_8.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a1/Terrace_of_Large_Victorian_Houses_-_geograph.org.uk_-_102849.jpg/640px-Terrace_of_Large_Victorian_Houses_-_geograph.org.uk_-_102849.jpg" }
};

            location5.Images = imagesLocation5;

            dbContext.Locations.Add(location5);

            var location6 = new Location
            {
                Title = "Rustiek Huisje aan het Meer",
                SubTitle = "Ontsnapping aan de drukte",
                Description = "Dit rustieke huisje ligt verscholen aan de oever van een schilderachtig meer en biedt een vredige en afgelegen omgeving. Met een knusse open haard en een eigen aanlegsteiger is het de perfecte plek voor een romantisch uitje.",
                Rooms = 3,
                NumberOfGuests = 6,
                PricePerDay = 500,
                Type = Location.LocationType.Cottage,
                Landlord = landlord2,
                AvailableFeatures = (Features)39
            };
            var imagesLocation6 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/Pilikula_Guthu_House_5.jpg/640px-Pilikula_Guthu_House_5.jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b4/Wikimania_accommodation_in_Ortanella_02.jpg/640px-Wikimania_accommodation_in_Ortanella_02.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5d/Pilikula_Guthu_House_2.jpg/640px-Pilikula_Guthu_House_2.jpg" }
};

            location6.Images = imagesLocation6;

            dbContext.Locations.Add(location6);

            var landlord3 = new Landlord
            {
                FirstName = "Sophia",
                LastName = "Johnson",
                Age = 45,
                Avatar = new Image
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fd/%D0%9F%D1%80%D0%B5%D0%B4%D0%BF%D1%80%D0%B8%D0%BD%D0%B8%D0%BC%D0%B0%D1%82%D0%B5%D0%BB%D1%8C%2C_%D0%B8%D0%BC%D0%B8%D0%B4%D0%B6-%D0%BF%D1%81%D0%B8%D1%85%D0%BE%D0%BB%D0%BE%D0%B3%2C_%D0%BE%D1%81%D0%BD%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D1%8C_%D0%B1%D1%80%D0%B5%D0%BD%D0%B4%D0%B0_Seven_pieces_%D0%98%D1%80%D0%B8%D0%BD%D0%B0_%D0%9B%D0%B5%D0%BE%D0%BD%D0%BE%D0%B2%D0%B0.jpg/640px-%D0%9F%D1%80%D0%B5%D0%B4%D0%BF%D1%80%D0%B8%D0%BD%D0%B8%D0%BC%D0%B0%D1%82%D0%B5%D0%BB%D1%8C%2C_%D0%B8%D0%BC%D0%B8%D0%B4%D0%B6-%D0%BF%D1%81%D0%B8%D1%85%D0%BE%D0%BB%D0%BE%D0%B3%2C_%D0%BE%D1%81%D0%BD%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D1%8C_%D0%B1%D1%80%D0%B5%D0%BD%D0%B4%D0%B0_Seven_pieces_%D0%98%D1%80%D0%B8%D0%BD%D0%B0_%D0%9B%D0%B5%D0%BE%D0%BD%D0%BE%D0%B2%D0%B0.jpg"
                }
            };
            dbContext.Images.Add(landlord3.Avatar);
            dbContext.Landlords.Add(landlord3);
            dbContext.SaveChanges();


            var location7 = new Location
            {
                Title = "Prachtig Strandhuis",
                SubTitle = "Direct aan de kust",
                Description = "Dit prachtige strandhuis biedt een panoramisch uitzicht op de oceaan en directe toegang tot het strand. Met 5 slaapkamers en een eigen zwembad is het ideaal voor een luxe strandvakantie.",
                Rooms = 5,
                NumberOfGuests = 10,
                PricePerDay = 1500,
                Type = Location.LocationType.House,
                Landlord = landlord3,
                AvailableFeatures = (Features)63
            };
            var imagesLocation7 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/More_generation_house_waldstadt_Karlsruhe.jpg/640px-More_generation_house_waldstadt_Karlsruhe.jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1c/Terraced_houses_-_geograph.org.uk_-_4409769.jpg/640px-Terraced_houses_-_geograph.org.uk_-_4409769.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Terraced_houses%2C_south_side_of_The_Causeway%2C_Grimsbury_-_geograph.org.uk_-_4407409.jpg/640px-Terraced_houses%2C_south_side_of_The_Causeway%2C_Grimsbury_-_geograph.org.uk_-_4407409.jpg" }
};
            location7.Images = imagesLocation7;

            dbContext.Locations.Add(location7);

            var location8 = new Location
            {
                Title = "Gezellig Chalet in de Bergen",
                SubTitle = "Omgeven door natuur",
                Description = "Dit gezellige chalet ligt hoog in de bergen en biedt een adembenemend uitzicht op de omliggende natuur. Met 3 slaapkamers en een knusse open haard is het de perfecte plek voor een rustige vakantie.",
                Rooms = 3,
                NumberOfGuests = 6,
                PricePerDay = 800,
                Type = Location.LocationType.Chalet,
                Landlord = landlord3,
                AvailableFeatures = (Features)47
            };
            var imagesLocation8 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b1/Small_house_for_birds_in_Marstrand..jpg/640px-Small_house_for_birds_in_Marstrand..jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8a/A_modern_terrace_of_town_houses_in_Killowen_village_-_geograph.org.uk_-_1891533.jpg/640px-A_modern_terrace_of_town_houses_in_Killowen_village_-_geograph.org.uk_-_1891533.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/House_of_O_M_Cheriyan_02.jpg/640px-House_of_O_M_Cheriyan_02.jpg" }
};

            location8.Images = imagesLocation8;

            dbContext.Locations.Add(location8);

            var location9 = new Location
            {
                Title = "Modern Appartement in het Centrum",
                SubTitle = "Stedelijk wonen op zijn best",
                Description = "Dit moderne appartement is gelegen in het bruisende stadscentrum en biedt alle gemakken van het stedelijke leven. Met 2 slaapkamers en een eigentijdse inrichting is het perfect voor stadsbewoners.",
                Rooms = 2,
                NumberOfGuests = 4,
                PricePerDay = 1000,
                Type = Location.LocationType.Appartment,
                Landlord = landlord3,
                AvailableFeatures = (Features)31
            };
            var imagesLocation9 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Matanaka_-_Granary%2C_Privy_%26_Schoolhouse.jpg/640px-Matanaka_-_Granary%2C_Privy_%26_Schoolhouse.jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Houses_by_Chatsworth_Road_-_geograph.org.uk_-_2441840.jpg/640px-Houses_by_Chatsworth_Road_-_geograph.org.uk_-_2441840.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Houses_on_Albert_Street_-_geograph.org.uk_-_3118031.jpg/640px-Houses_on_Albert_Street_-_geograph.org.uk_-_3118031.jpg" }
};

            location9.Images = imagesLocation9;

            dbContext.Locations.Add(location9);

            var location10 = new Location
            {
                Title = "Gezellige Cottage aan het Meer",
                SubTitle = "Een rustige toevluchtsoord",
                Description = "Deze gezellige cottage ligt aan de oever van een schilderachtig meer en biedt een rustige en ontspannen omgeving. Met een ruime veranda en een eigen aanlegsteiger is het ideaal voor een ontspannen vakantie.",
                Rooms = 4,
                NumberOfGuests = 8,
                PricePerDay = 1200,
                Type = Location.LocationType.Cottage,
                Landlord = landlord3,
                AvailableFeatures = (Features)39
            };
            var imagesLocation10 = new ObservableCollection<Image>
{
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4a/Houses_of_Holme_Street%2C_Wakefield_Road_-_geograph.org.uk_-_6014081.jpg/640px-Houses_of_Holme_Street%2C_Wakefield_Road_-_geograph.org.uk_-_6014081.jpg", IsCover = true },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Back_to_Back_houses_off_Cross_Lane_-_geograph.org.uk_-_5183230.jpg/640px-Back_to_Back_houses_off_Cross_Lane_-_geograph.org.uk_-_5183230.jpg" },
    new Image { Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Brynffynnon_-_a_terrace_of_Victorian_houses_-_geograph.org.uk_-_1931582.jpg/640px-Brynffynnon_-_a_terrace_of_Victorian_houses_-_geograph.org.uk_-_1931582.jpg" }
};

            location10.Images = imagesLocation10;

            dbContext.Locations.Add(location10);


            dbContext.SaveChanges();

        }
    }
}

