namespace SchoolLibrary.DataAccess.Mappers
{
    using System;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ReservedItemMapper : IMapper<ReservedItem, ReservedItemBusinessModel>
    {
        public ReservedItem Map(ReservedItemBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }


            var destination = new ReservedItem()
            {
                Id = source.Id,
                Reader = new ReaderMapper().Map(source.Reader),
                Date = new DateTime(source.Date.Value.Ticks),
                IsReady = source.IsReady,
                ReadyDate = source.ReadyDate == null ? (DateTime?)null : new DateTime(source.ReadyDate.Value.Ticks)
            };

            if (source.Item is BookBusinessModel)
            {
                destination.Item = new BookMapper().Map(source.Item as BookBusinessModel);
            }
            else if (source.Item is MagazineBusinessModel)
            {
                destination.Item = new MagazineMapper().Map(source.Item as MagazineBusinessModel);
            }
            else if (source.Item is DiskBusinessModel)
            {
                destination.Item=new DiskMapper().Map(source.Item as DiskBusinessModel);
            }

            return destination;
        }

        public ReservedItemBusinessModel Map(ReservedItem source)
        {
            if (source == null)
            {
                return null;
            }


            var destination = new ReservedItemBusinessModel()
            {
                Id = source.Id,
                Reader = new ReaderMapper().Map(source.Reader),
                Date = new DateTime(source.Date.Value.Ticks),
                IsReady = source.IsReady,
                ReadyDate = source.ReadyDate == null ? (DateTime?)null : new DateTime(source.ReadyDate.Value.Ticks),
               
            };

            if (source.Item is Book)
            {
                destination.Item = new BookMapper().Map(source.Item as Book);

                destination.Category = "Book";

                destination.Name = source.Item.Name;
            }
            else if (source.Item is Magazine)
            {
                destination.Item = new MagazineMapper().Map(source.Item as Magazine);

                destination.Category = "Magazine";

                destination.Name = source.Item.Name;
            }
            else if (source.Item is Disk)
            {
                destination.Item = new DiskMapper().Map(source.Item as Disk);

                destination.Category = "Disk";

                destination.Name = source.Item.Name;
            }

            return destination;
        }
    }
}