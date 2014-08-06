namespace SchoolLibrary.Tests.Fakes
{
    using Moq;
    using SchoolLibrary.DataAccess.Context;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Repository;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public static class Initializer
    {
        public static ILibraryContext GetContext()
        {
            var authors = new AuthorsSet();
            var items = new ItemsSet();
            var inventory = new InventorySet();
            var consignment = new ConsignmentSet();
            var readers = new ReaderSet();
            var readerHistory = new ReaderHistorySet();
            var reservedItem = new ReservedItemSet();
            var scannedPage = new ScannedPageSet();
            var userProfiles = new UserProfileSet();
            var tags = new TagsSet();
            var tagScores = new TagScoresSet();

            var context = new Mock<ILibraryContext>();
            context.Setup(c => c.Authors).Returns(authors);
            context.Setup(c => c.Consignment).Returns(consignment);
            context.Setup(c => c.Items).Returns(items);
            context.Setup(c => c.Inventory).Returns(inventory);
            context.Setup(c => c.Readers).Returns(readers);
            context.Setup(c => c.ReaderHistory).Returns(readerHistory);
            context.Setup(c => c.ReservedItem).Returns(reservedItem);
            context.Setup(c => c.ScannedPage).Returns(scannedPage);
            context.Setup(c => c.UserProfiles).Returns(userProfiles);
            context.Setup(c => c.Tags).Returns(tags);
            context.Setup(c => c.TagScores).Returns(tagScores);
            return context.Object;
        }

        public static IRepository<T> GetRepository<T>() where T : class
        {
            return new RepositoryFake<T>(Initializer.GetContext());
        }

        public static ILibraryUow GetLibraryUow()
        {
            var uow = new Mock<ILibraryUow>();
            uow.Setup(u => u.Authors).Returns(Initializer.GetRepository<Author>());
            uow.Setup(u => u.Consignments).Returns(Initializer.GetRepository<Consignment>());
            uow.Setup(u => u.Inventories).Returns(Initializer.GetRepository<Inventory>());
            uow.Setup(u => u.Items).Returns(Initializer.GetRepository<Item>());
            uow.Setup(u => u.Readers).Returns(Initializer.GetRepository<Reader>());
            uow.Setup(u => u.ReservedItems).Returns(Initializer.GetRepository<ReservedItem>());
            uow.Setup(u => u.ReadersHistories).Returns(Initializer.GetRepository<ReaderHistory>());
            uow.Setup(u => u.ScannedPages).Returns(Initializer.GetRepository<ScannedPage>());
            uow.Setup(u => u.TagScores).Returns(Initializer.GetRepository<TagScore>());
            uow.Setup(u => u.Tags).Returns(Initializer.GetRepository<Tag>());
            uow.Setup(u => u.UsersProfiles).Returns(Initializer.GetRepository<UserProfile>());
            return uow.Object;
        }
    }
}