namespace SchoolLibrary.DataAccess.Context
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using SchoolLibrary.DataAccess.Entities;

    /// <summary>
    /// Library Context Initializer - in progress
    /// </summary>
    public class LibraryContextInitializer : DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        public void getSeedMethod(LibraryContext context)
        {
            this.Seed(context);
        }

        protected override void Seed(LibraryContext context)
        {
            //SeedMembership();

            base.Seed(context);
            context.Database.ExecuteSqlCommand("CREATE INDEX IX_Authors ON Authors (LastName, FirstName, MiddleName)");
            context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IX_Consignment_Number ON Consignment ( Number )");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER InsConsignNumTrigger on Consignment " +
                            "FOR INSERT AS BEGIN DECLARE @maximum INT; DECLARE @id INT; " +
                            "set @maximum =(select MAX(Consignment.Number) from Consignment)+1 " +
                            "SET @id = (SELECT Id FROM Inserted) update Consignment " +
                            "SET Number=@maximum where Id=@id " + "END");
            context.Database.ExecuteSqlCommand("CREATE INDEX IX_Inventory_Number ON Inventory ( Number )");

            #region tags

            var drama = new Tag { Name = "Drama" };
            context.Tags.Add(drama);
            var fable = new Tag { Name = "Fable" };
            context.Tags.Add(fable);
            var tale = new Tag { Name = "Fairy tale" };
            context.Tags.Add(tale);
            var fantasy = new Tag { Name = "Fantasy" };
            context.Tags.Add(fantasy);
            var folklore = new Tag { Name = "Folklore" };
            context.Tags.Add(folklore);
            var horror = new Tag { Name = "Horror" };
            context.Tags.Add(horror);
            var humor = new Tag { Name = "Humor" };
            context.Tags.Add(humor);
            var legend = new Tag { Name = "Legend" };
            context.Tags.Add(legend);
            var mystery = new Tag { Name = "Mystery" };
            context.Tags.Add(mystery);
            var myth = new Tag { Name = "Mythology" };
            context.Tags.Add(myth);
            var poetry = new Tag { Name = "Poetry" };
            context.Tags.Add(poetry);
            var ficiton = new Tag { Name = "Science fiction" };
            context.Tags.Add(ficiton);

            #endregion

            #region readers
            var tags = new Collection<Tag>();
            tags.Add(drama);
            tags.Add(legend);
            tags.Add(fable);
            tags.Add(ficiton);
            Reader alice = new Reader
            {
                FirstName = "Alice",
                LastName = "Hopkins",
                Address = "238-9685 Accumsan Rd.",
                Birthday = new DateTime(2006, 09, 19),
                Phone = "067-293-21-88",
                EMail = "alice@gmail.com",
                Preferences = tags
            };
            Reader hillary = new Reader
            {
                FirstName = "Hillary",
                LastName = "Morgan",
                Address = "200-4805 Ac Street",
                Birthday = new DateTime(2003, 07, 09),
                Phone = "067-688-28-57",
                EMail = "hillary@gmail.com"
            };
            Reader ralph = new Reader
            {
                FirstName = "Ralph",
                LastName = "Rosa",
                Address = "579-3862 In Rd.",
                Birthday = new DateTime(2005, 11, 23),
                Phone = "063-374-67-44",
                EMail = "ralph@gmail.com"
            };
            Reader stephen = new Reader
            {
                FirstName = "Stephen",
                LastName = "Floyd",
                Address = "681-1610 Velit St.",
                Birthday = new DateTime(2002, 02, 28),
                Phone = "063-845-73-59",
                EMail = "Stephen_F@gmail.com"
            };
            Reader evan = new Reader
            {
                FirstName = "Evan",
                LastName = "Caldwell",
                Address = "3989 Eget Rd.",
                Birthday = new DateTime(2001, 04, 17),
                Phone = "067-931-86-62",
                EMail = "Evan_C@gmail.com"
            };
            Reader kevin = new Reader
            {
                FirstName = "Kevin",
                LastName = "Levrone",
                Address = "784 Ducas str.",
                Birthday = new DateTime(2002, 04, 11),
                Phone = "096-089-42-12",
                EMail = "kevin@gmail.com"
            };
            Reader mykola = new Reader
            {
                FirstName = "Mykola",
                LastName = "Stepanyak",
                Address = "8888 Eget Rd.",
                Birthday = new DateTime(2001, 04, 17),
                Phone = "067-932-22-62",
                EMail = "mykola@gmail.com"
            };
            Reader mike = new Reader
            {
                FirstName = "Mike",
                LastName = "Evans",
                Address = "3 Vulis road",
                Birthday = new DateTime(2002, 03, 01),
                Phone = "050-547-89-20",
                EMail = "evans@gmail.com"
            };
            Reader rick = new Reader
            {
                FirstName = "Rick",
                LastName = "Douglas",
                Address = "541 Village str.",
                Birthday = new DateTime(2007, 07, 28),
                Phone = "098-098-45-23",
                EMail = "rick@gmail.com"
            };
            Reader john = new Reader
            {
                FirstName = "John",
                LastName = "Jobs",
                Address = "12 Sil.Dol.",
                Birthday = new DateTime(2002, 11, 4),
                Phone = "063-190-78-20",
                EMail = "john@gmail.com"
            };
            Reader marry = new Reader
            {
                FirstName = "Mary",
                LastName = "Crist",
                Address = "654 Hollywood road",
                Birthday = new DateTime(2001, 12, 31),
                Phone = "063-457-85-20",
                EMail = "mary@gmail.com"
            };


            Reader kola = new Reader
            {
                FirstName = "Mykola",
                LastName = "Ivanovich",
                Address = "8888 Zelena St.",
                Birthday = new DateTime(2002, 04, 17),
                Phone = "063-935-21-65",
                EMail = "kola@gmail.com"
            };

            Reader stepan = new Reader
            {
                FirstName = "Stepan",
                LastName = "Stepanov",
                Address = "8888 Str St.",
                Birthday = new DateTime(2004, 04, 17),
                Phone = "063-935-21-65",
                EMail = "stepan@gmail.com"
            };

            context.Readers.Add(alice);
            context.Readers.Add(hillary);
            context.Readers.Add(ralph);
            context.Readers.Add(stephen);
            context.Readers.Add(evan);
            context.Readers.Add(mykola);
            context.Readers.Add(kola);

            context.Readers.Add(rick);
            context.Readers.Add(marry);
            context.Readers.Add(kevin);
            context.Readers.Add(john);
            context.Readers.Add(mike);
            context.Readers.Add(stepan);
            #endregion

            #region books, disks, magazines, authors

            Author keelie = new Author { FirstName = "Keelie", LastName = "Mcmillan" };
            Author dara = new Author { FirstName = "Dara", LastName = "Mcpherson" };
            Author nell = new Author { FirstName = "Nell", LastName = "Holder" };
            Author omar = new Author { FirstName = "Omar", LastName = "Hines" };
            Author ivan = new Author { FirstName = "Ivan", LastName = "Garrett" };

            tags = new Collection<Tag>();
            tags.Add(drama);
            tags.Add(poetry);
            tags.Add(legend);

            Book lacus = new Book
            {
                Name = "Hedgehog in the fog",
                Year = 1974,
                Publisher = "Pelago",
                PageCount = 637,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(humor);
            tags.Add(fable);
            tags.Add(folklore);
            tags.Add(fantasy);
            Book duis = new Book
            {
                Name = "Star Wars: Attack of the Entities",
                Year = 1900,
                Publisher = "Loverval",
                PageCount = 338,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(drama);
            tags.Add(horror);
            tags.Add(ficiton);
            tags.Add(mystery);
            Book proin = new Book
            {
                Name = "Games of the Thrones",
                Year = 2008,
                Publisher = "Palestrina",
                PageCount = 927,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(folklore);
            tags.Add(humor);
            tags.Add(poetry);
            Book magna = new Book
            {
                Name = "Hobbit the journey from Entities to MVC",
                Year = 2000,
                Publisher = "Aurora",
                PageCount = 658,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(mystery);
            tags.Add(ficiton);
            tags.Add(drama);
            Book lectus = new Book
            {
                Name = "Harry Potter and School Library",
                Year = 1992,
                Publisher = "Toronto",
                PageCount = 259,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(tale);
            tags.Add(legend);
            tags.Add(drama);
            Book fifty = new Book
            {
                Name = "Fifty Shades of Grey",
                Year = 1992,
                Publisher = "Toronto",
                PageCount = 259,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(mystery);
            tags.Add(horror);
            tags.Add(fantasy);
            Book beautiful = new Book
            {
                Name = "Beautiful Disaster",
                Year = 1992,
                Publisher = "Toronto",
                PageCount = 259,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(poetry);
            tags.Add(folklore);
            tags.Add(legend);
            Book pride = new Book
            {
                Name = "Pride and Prejudice",
                Year = 1992,
                Publisher = "Toronto",
                PageCount = 259,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(tale);
            tags.Add(fable);
            tags.Add(drama);
            Book twilight = new Book
            {
                Name = "Adventures of Lv-098 in Lazy Loading Land",
                Year = 1992,
                Publisher = "Toronto",
                PageCount = 259,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(ficiton);
            tags.Add(myth);
            Book perfect = new Book
            {
                Name = "Lv-098 fighting Mappers",
                Year = 1992,
                Publisher = "Toronto",
                PageCount = 259,
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(drama);
            tags.Add(fantasy);
            Disk disk = new Disk
            {
                Name = "Total English",
                Year = 2000,
                Producer = "Pilowar",
                Type = "DVD",
                Tags = tags
            };

            tags = new Collection<Tag>();
            tags.Add(humor);
            tags.Add(myth);
            Magazine magazine = new Magazine
            {
                Name = "Cosmopolitan",
                Publisher = "Folio",
                Issue = 12,
                Year = 2005,
                Tags = tags
            };


            lacus.Authors.Add(keelie);
            lacus.Authors.Add(nell);
            magna.Authors.Add(keelie);
            magna.Authors.Add(omar);
            duis.Authors.Add(dara);
            proin.Authors.Add(nell);
            lectus.Authors.Add(ivan);
            perfect.Authors.Add(keelie);
            twilight.Authors.Add(nell);
            pride.Authors.Add(omar);
            beautiful.Authors.Add(ivan);
            fifty.Authors.Add(omar);



            context.Items.Add(lacus);
            context.Items.Add(magna);
            context.Items.Add(duis);
            context.Items.Add(proin);
            context.Items.Add(lectus);
            context.Items.Add(fifty);
            context.Items.Add(beautiful);
            context.Items.Add(pride);
            context.Items.Add(twilight);
            context.Items.Add(perfect);

            context.Items.Add(disk);
            context.Items.Add(magazine);
            //context.Authors.Add(keelie);
            //context.Authors.Add(nell);
            //context.Authors.Add(omar);
            //context.Authors.Add(dara);
            //context.Authors.Add(ivan);

            #endregion

            #region inventory

            Consignment party1 = new Consignment { Item = lacus, ArrivalDate = DateTime.Now };
            Inventory lacus1 = new Inventory { Item = lacus, Number = "0000000001-0001" };
            Inventory lacus2 = new Inventory { Item = lacus, Number = "0000000001-0002" };
            party1.Inventories.Add(lacus1);
            party1.Inventories.Add(lacus2);

            Consignment party2 = new Consignment { Item = magna, ArrivalDate = DateTime.Now };
            Inventory magna1 = new Inventory { Item = magna, Number = "0000000002-0001" };
            Inventory magna2 = new Inventory { Item = magna, Number = "0000000002-0002" };
            Inventory magna3 = new Inventory { Item = magna, Number = "0000000002-0003" };
            party2.Inventories.Add(magna1);
            party2.Inventories.Add(magna2);
            party2.Inventories.Add(magna3);

            Consignment party3 = new Consignment { Item = duis, ArrivalDate = DateTime.Now };
            Inventory duis1 = new Inventory { Item = duis, Number = "0000000003-0001" };
            party3.Inventories.Add(duis1);

            Consignment party4 = new Consignment { Item = proin, ArrivalDate = DateTime.Now };
            Inventory proin1 = new Inventory { Item = proin, Number = "0000000004-0001" };
            party4.Inventories.Add(proin1);

            Consignment party5 = new Consignment { Item = lectus, ArrivalDate = DateTime.Now };
            Inventory lectus1 = new Inventory { Item = lectus, Number = "0000000005-0001" };
            party5.Inventories.Add(lectus1);

            Consignment party6 = new Consignment { Item = fifty, ArrivalDate = DateTime.Now };
            Inventory fifty1 = new Inventory { Item = fifty, Number = "0000000006-0001" };
            Inventory fifty2 = new Inventory { Item = fifty, Number = "0000000006-0002" };
            Inventory fifty3 = new Inventory { Item = fifty, Number = "0000000006-0003" };
            Inventory fifty4 = new Inventory { Item = fifty, Number = "0000000006-0004" };
            party6.Inventories.Add(fifty1);
            party6.Inventories.Add(fifty2);
            party6.Inventories.Add(fifty3);
            party6.Inventories.Add(fifty4);

            Consignment party7 = new Consignment { Item = beautiful, ArrivalDate = DateTime.Now };
            Inventory beautiful1 = new Inventory { Item = beautiful, Number = "0000000007-0001" };
            Inventory beautiful2 = new Inventory { Item = beautiful, Number = "0000000007-0002" };
            Inventory beautiful3 = new Inventory { Item = beautiful, Number = "0000000007-0003" };

            party7.Inventories.Add(beautiful1);
            party7.Inventories.Add(beautiful2);
            party7.Inventories.Add(beautiful3);

            Consignment party8 = new Consignment { Item = pride, ArrivalDate = DateTime.Now };
            Inventory pride1 = new Inventory { Item = pride, Number = "0000000008-0001" };
            Inventory pride2 = new Inventory { Item = pride, Number = "0000000008-0002" };
            Inventory pride3 = new Inventory { Item = pride, Number = "0000000008-0003" };
            Inventory pride4 = new Inventory { Item = pride, Number = "0000000008-0004" };
            Inventory pride5 = new Inventory { Item = pride, Number = "0000000008-0005" };
            party8.Inventories.Add(pride1);
            party8.Inventories.Add(pride2);
            party8.Inventories.Add(pride3);
            party8.Inventories.Add(pride4);
            party8.Inventories.Add(pride5);

            Consignment party9 = new Consignment { Item = twilight, ArrivalDate = DateTime.Now };
            Inventory twilight1 = new Inventory { Item = twilight, Number = "0000000009-0001" };
            Inventory twilight2 = new Inventory { Item = twilight, Number = "0000000009-0002" };
            Inventory twilight3 = new Inventory { Item = twilight, Number = "0000000009-0003" };
            Inventory twilight4 = new Inventory { Item = twilight, Number = "0000000009-0004" };
            Inventory twilight5 = new Inventory { Item = twilight, Number = "0000000009-0005" };
            party9.Inventories.Add(twilight1);
            party9.Inventories.Add(twilight2);
            party9.Inventories.Add(twilight3);
            party9.Inventories.Add(twilight4);
            party9.Inventories.Add(twilight5);

            Consignment party10 = new Consignment { Item = perfect, ArrivalDate = DateTime.Now };
            Inventory perfect1 = new Inventory { Item = perfect, Number = "0000000010-0001" };
            Inventory perfect2 = new Inventory { Item = perfect, Number = "0000000010-0002" };
            Inventory perfect3 = new Inventory { Item = perfect, Number = "0000000010-0003" };
            Inventory perfect4 = new Inventory { Item = perfect, Number = "0000000010-0004" };
            Inventory perfect5 = new Inventory { Item = perfect, Number = "0000000010-0005" };
            party10.Inventories.Add(perfect1);
            party10.Inventories.Add(perfect2);
            party10.Inventories.Add(perfect3);
            party10.Inventories.Add(perfect4);
            party10.Inventories.Add(perfect5);

            Consignment party11 = new Consignment { Item = disk, ArrivalDate = DateTime.Now };
            Inventory disk1 = new Inventory { Item = disk, Number = "0000000011-0001" };
            party11.Inventories.Add(disk1);

            Consignment party12 = new Consignment { Item = magazine, ArrivalDate = DateTime.Now };
            Inventory magazine1 = new Inventory { Item = magazine, Number = "0000000012-0001" };
            Inventory magazine2 = new Inventory { Item = magazine, Number = "0000000012-0002" };
            party12.Inventories.Add(magazine1);
            party12.Inventories.Add(magazine2);

            magna1.IsAvailable = false;
            lacus2.IsAvailable = false;
            perfect1.IsAvailable = false;
            magazine2.IsAvailable = false;
            duis1.IsAvailable = false;
            pride3.IsAvailable = false;
            fifty2.IsAvailable = false;

            context.Consignment.Add(party1);
            context.Consignment.Add(party2);
            context.Consignment.Add(party3);
            context.Consignment.Add(party4);
            context.Consignment.Add(party5);
            context.Consignment.Add(party6);
            context.Consignment.Add(party7);
            context.Consignment.Add(party8);
            context.Consignment.Add(party9);
            context.Consignment.Add(party10);
            context.Consignment.Add(party11);
            context.Consignment.Add(party12);


            //context.Inventory.Add(lacus1);
            //context.Inventory.Add(lacus2);
            //context.Inventory.Add(magna1);
            //context.Inventory.Add(magna2);
            //context.Inventory.Add(duis1);
            //context.Inventory.Add(proin1);
            //context.Inventory.Add(lectus1);

            #endregion

            #region reader history

            var rh = new List<ReaderHistory>
                             {
                                 new ReaderHistory
                                     {
                                         Reader = alice,
                                         Inventory = lacus1,
                                         StartDate =
                                             new DateTime(2013, 07, 29, 08, 06, 13),
                                         ReturnDate =
                                             new DateTime(2013, 08,15, 08, 25, 06),
                                         FinishDate =
                                             new DateTime(2013, 12, 10, 11, 13, 32)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = alice,
                                         Inventory = duis1,
                                         StartDate =
                                             new DateTime(2013, 06, 14, 06, 58, 18), 
                                         //ReturnDate = new DateTime(2013, 07, 29, 11, 34, 24), 
                                         FinishDate =
                                             new DateTime(2013, 12, 20, 22, 25, 28)
                                     },
                                 new ReaderHistory
                                     {
                                         Reader = alice,
                                         Inventory = magna1,
                                         StartDate =
                                             new DateTime(2013, 03, 14, 06, 58, 18), 
                                         //ReturnDate = new DateTime(2013, 06, 29, 11, 34, 24), 
                                         FinishDate =
                                             new DateTime(2013, 12, 25, 22, 25, 28)
                                     },
                                 new ReaderHistory
                                     {
                                         Reader = alice,
                                         Inventory = fifty1,
                                         StartDate =
                                             new DateTime(2013, 04, 16, 06, 58, 18), 
                                         ReturnDate = new DateTime(2013, 07, 29, 11, 34, 24), 
                                         FinishDate =
                                             new DateTime(2013, 11, 23, 22, 25, 28)
                                     },

                                 new ReaderHistory
                                     {
                                         Reader = hillary,
                                         Inventory = magna2,
                                         StartDate =
                                             new DateTime(2013, 07, 04, 15, 04, 48), 
                                         ReturnDate = new DateTime(2013, 08, 19, 06, 55, 34), 
                                         FinishDate =
                                             new DateTime(2013, 11, 20, 05, 13, 33)
                                     },
                                 new ReaderHistory
                                     {
                                         Reader = hillary,
                                         Inventory = fifty3,
                                         StartDate =
                                             new DateTime(2013, 07, 13, 15, 04, 48), 
                                         ReturnDate = new DateTime(2013, 08, 12, 06, 55, 34), 
                                         FinishDate =
                                             new DateTime(2013, 11, 10, 05, 13, 33)
                                     },
                                  new ReaderHistory
                                     {
                                         Reader = hillary,
                                         Inventory = beautiful1,
                                         StartDate =
                                             new DateTime(2013, 02, 13, 15, 04, 48), 
                                         ReturnDate = new DateTime(2013, 08, 11, 06, 55, 34), 
                                         FinishDate =
                                             new DateTime(2013, 12, 10, 05, 13, 33)
                                     },
                                   new ReaderHistory
                                     {
                                         Reader = hillary,
                                         Inventory = pride1,
                                         StartDate =
                                             new DateTime(2013, 06, 13, 15, 04, 48), 
                                         ReturnDate = new DateTime(2013, 08, 11, 06, 55, 34), 
                                         FinishDate =
                                             new DateTime(2013, 12, 8, 05, 13, 33)
                                     },

                                 new ReaderHistory
                                     {
                                         Reader = stephen,
                                         Inventory = lectus1,
                                         StartDate =
                                             new DateTime(2013, 09, 24, 11, 56, 32),
                                         ReturnDate =
                                             new DateTime(2013, 10, 11, 10, 08, 31),
                                         FinishDate =
                                             new DateTime(2013, 12, 08, 08, 54, 52)
                                     },
                                 new ReaderHistory
                                     {
                                         Reader = stephen,
                                         Inventory = beautiful3,
                                         StartDate =
                                             new DateTime(2013, 09, 13, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 10, 11, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 12, 15, 23, 31, 10)
                                     },
                                 new ReaderHistory
                                     {
                                         Reader = stephen,
                                         Inventory = pride3,
                                         StartDate =
                                             new DateTime(2013, 09, 11, 14, 38, 57),
                                         //ReturnDate =
                                         //    new DateTime(2013, 10, 11, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 15, 23, 31, 10)
                                     },
                                 new ReaderHistory
                                     {
                                         Reader = stephen,
                                         Inventory = magna3,
                                         StartDate =
                                             new DateTime(2013, 05, 11, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 10, 11, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 19, 23, 31, 10)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = ralph,
                                         Inventory = lacus2,
                                         StartDate =
                                             new DateTime(2013, 05, 11, 14, 38, 57),
                                         //ReturnDate =
                                         //    new DateTime(2013, 10, 11, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 19, 23, 31, 10)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = ralph,
                                         Inventory = fifty2,
                                         StartDate =
                                             new DateTime(2013, 05, 16, 14, 38, 57),
                                         //ReturnDate =
                                         //    new DateTime(2013, 10, 15, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 29, 23, 31, 10)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = ralph,
                                         Inventory = beautiful2,
                                         StartDate =
                                             new DateTime(2013, 03, 16, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 10, 20, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 12, 14, 23, 31, 10)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = ralph,
                                         Inventory = pride2,
                                         StartDate =
                                             new DateTime(2013, 01, 16, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 10, 21, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 12, 6, 23, 31, 10)
                                     },

                                new ReaderHistory
                                     {
                                         Reader = evan,
                                         Inventory = pride4,
                                         StartDate =
                                             new DateTime(2013, 02, 26, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 09, 21, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 12, 12, 23, 31, 10)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = evan,
                                         Inventory = twilight1,
                                         StartDate =
                                             new DateTime(2013, 02, 21, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 04, 21, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 12, 18, 23, 31, 10)
                                     },
                               new ReaderHistory
                                     {
                                         Reader = evan,
                                         Inventory = perfect1,
                                         StartDate =
                                             new DateTime(2013, 02, 24, 14, 38, 57),
                                         //ReturnDate =
                                         //    new DateTime(2013, 06, 21, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 18, 23, 31, 10)
                                     },
                               new ReaderHistory
                                     {
                                         Reader = evan,
                                         Inventory = magazine1,
                                         StartDate =
                                             new DateTime(2013, 05, 28, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 06, 25, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 26, 23, 31, 10)
                                     },

                                new ReaderHistory
                                     {
                                         Reader = mykola,
                                         Inventory = perfect2,
                                         StartDate =
                                             new DateTime(2013, 04, 28, 14, 38, 57),
                                         ReturnDate =
                                             new DateTime(2013, 06, 25, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 12, 16, 23, 31, 10)
                                     },
                                new ReaderHistory
                                     {
                                         Reader = mykola,
                                         Inventory = magazine2,
                                         StartDate =
                                             new DateTime(2013, 05, 28, 14, 38, 57),
                                         //ReturnDate =
                                         //    new DateTime(2013, 06, 25, 06, 19, 23),
                                         FinishDate =
                                             new DateTime(2013, 11, 16, 23, 31, 10)
                                     }
                             };
            rh.ForEach(r => context.ReaderHistory.Add(r));

            #endregion

            #region ScannedPage

            ScannedPage skp1 = new ScannedPage
            {
                Item = lacus,
                Reader = mykola,
                OrderText = "{1}-{3}(1); {22}-{33}(1);",
                OrderDate = new DateTime(2013, 11, 19),
                Message = "",
            };

            ScannedPage skp2 = new ScannedPage
            {
                Item = lacus,
                Reader = mykola,
                OrderText = "{22}-{23}(1); {24}-{39}(1);",
                OrderDate = new DateTime(2013, 11, 19),
                Message = "",
            };

            ScannedPage skp3 = new ScannedPage
            {
                Item = duis,
                Reader = mykola,
                OrderText = "{44}-{55}(1);",
                OrderDate = new DateTime(2013, 11, 20),
                Message = "",
            };

            ScannedPage skp4 = new ScannedPage
            {
                Item = duis,
                Reader = mykola,
                OrderText = "{14}-{18}(1);",
                OrderDate = new DateTime(2013, 11, 21),
                IsLocked = true,
                Message = "",
            };

            ScannedPage skp5 = new ScannedPage
            {
                Item = duis,
                Reader = mykola,
                OrderText = "{23}-{35}(1);",
                OrderDate = new DateTime(2013, 11, 23),
                ExecutionDate = new DateTime(2013, 11, 24),
                IsLocked = true,
                IsReady = true,
                Message = "Ready!",
            };

            ScannedPage skpkola1 = new ScannedPage
            {
                Item = lacus,
                Reader = kola,
                OrderText = "{1}-{3}(1); {22}-{33}(1);",
                OrderDate = new DateTime(2013, 11, 19),
                Message = "",
            };

            ScannedPage skpkola2 = new ScannedPage
            {
                Item = lacus,
                Reader = kola,
                OrderText = "{22}-{23}(1); {24}-{39}(1);",
                OrderDate = new DateTime(2013, 11, 19),
                Message = "",
            };

            ScannedPage skpkola3 = new ScannedPage
            {
                Item = duis,
                Reader = kola,
                OrderText = "{56}-{66}(1); {78}-{80}(1);",
                OrderDate = new DateTime(2013, 11, 19),
                Message = "",
            };

            context.ScannedPage.Add(skp1);
            context.ScannedPage.Add(skp2);
            context.ScannedPage.Add(skp3);
            context.ScannedPage.Add(skp4);
            context.ScannedPage.Add(skp5);
            context.ScannedPage.Add(skpkola1);
            context.ScannedPage.Add(skpkola2);

            #endregion

            var rb = new List<ReservedItem>
                {
                    new ReservedItem { Reader = ralph, Item = lacus, Date = new DateTime(2012, 07, 29, 08, 06, 13), },
                    new ReservedItem { Reader = stephen, Item = magna, Date = new DateTime(2012, 10, 29, 08, 06, 13), }
                };
            rb.ForEach(r => context.ReservedItem.Add(r));

            context.SaveChanges();
        }
    }
}