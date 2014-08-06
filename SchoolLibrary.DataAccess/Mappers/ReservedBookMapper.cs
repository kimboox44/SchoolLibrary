namespace SchoolLibrary.DataAccess.Mappers
{
    using System;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ReservedBookMapper : IMapper<ReservedBook, ReservedBookBusinessModel>
    {
        public ReservedBook Map(ReservedBookBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = new ReservedBook
            {
                Id = source.Id,
                Book = new BookMapper().Map(source.Book),
                Reader = new ReaderMapper().Map(source.Reader),
                Date = new DateTime(source.Date.Value.Ticks),
                IsReady = source.IsReady,
                ReadyDate = source.ReadyDate == null ? (DateTime?)null : new DateTime(source.ReadyDate.Value.Ticks)
            };

            return destination;
        }

        public ReservedBookBusinessModel Map(ReservedBook source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = new ReservedBookBusinessModel
            {
                Id = source.Id,
                Book = new BookMapper().Map(source.Book),
                Reader = new ReaderMapper().Map(source.Reader),
                Date = new DateTime(source.Date.Value.Ticks),
                IsReady = source.IsReady,
                ReadyDate = source.ReadyDate == null ? (DateTime?)null : new DateTime(source.ReadyDate.Value.Ticks)
            };

            return destination;
        }
    }
}