namespace SchoolLibrary.DataAccess.Mappers
{
    using System;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ScannedPageMapper : IMapper<ScannedPage, ScannedPageBusinessModel>
    {
        public ScannedPage Map(ScannedPageBusinessModel source)
        {
            if (source == null) return null;
            Nullable<DateTime> myDateTime = null;

            var destination = new ScannedPage()
            {
                Id = source.Id,
                OrderText = source.OrderText,
                OrderDate = new DateTime(source.OrderDate.Value.Ticks),
                ExecutionDate = source.ExecutionDate.HasValue ? new DateTime(source.ExecutionDate.Value.Ticks) : myDateTime,
                IsReady = source.IsReady,
                IsLocked = source.IsLocked,
                Message = source.Message,
                Item = new ItemMapper().Map(source.Item),
                Reader = new ReaderMapper().Map(source.Reader)
            };

            return destination;
        }

        public ScannedPageBusinessModel Map(ScannedPage source)
        {
            if (source == null) return null;
            Nullable<DateTime> myDateTime = null;

            var destination = new ScannedPageBusinessModel()
            {
                Id = source.Id,
                OrderText = source.OrderText,
                OrderDate = new DateTime(source.OrderDate.Value.Ticks),
                ExecutionDate = source.ExecutionDate.HasValue ? new DateTime(source.ExecutionDate.Value.Ticks) : myDateTime,
                IsReady = source.IsReady,
                IsLocked = source.IsLocked,
                Message = source.Message,
                Item = new ItemMapper().Map(source.Item),
                Reader = new ReaderMapper().Map(source.Reader)
            };

            return destination;
        }
    }
}
