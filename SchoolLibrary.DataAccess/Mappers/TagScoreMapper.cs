namespace SchoolLibrary.DataAccess.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class TagScoreMapper : IMapper<TagScore, TagScoreBusinessModel>
    {
        public TagScore Map(TagScoreBusinessModel source)
        {
            TagScore destination = new TagScore();
            if (source.Item is BookBusinessModel)
            {
                BookWithoutTagsMapper bookMapper = new BookWithoutTagsMapper();
                destination.Item = bookMapper.Map(source.Item as BookBusinessModel);
            }

            if (source.Item is DiskBusinessModel)
            {
                DiskMapper diskMapper = new DiskMapper();
                destination.Item = diskMapper.Map(source.Item as DiskBusinessModel);
            }

            if (source.Item is MagazineBusinessModel)
            {
                MagazineMapper magazineMapper = new MagazineMapper();
                destination.Item = magazineMapper.Map(source.Item as MagazineBusinessModel);
            }

            ReaderMapper readerMapper = new ReaderMapper();
            destination.Reader = readerMapper.Map(source.Reader);

            destination.Score = source.Score;

            return destination;
        }

        public TagScoreBusinessModel Map(TagScore source)
        {
            TagScoreBusinessModel destination = new TagScoreBusinessModel();
            if (source.Item is Book)
            {
                BookWithoutTagsMapper bookMapper = new BookWithoutTagsMapper();
                destination.Item = bookMapper.Map(source.Item as Book);
            }

            if (source.Item is Disk)
            {
                DiskMapper diskMapper = new DiskMapper();
                destination.Item = diskMapper.Map(source.Item as Disk);
            }

            if (source.Item is Magazine)
            {
                MagazineMapper magazineMapper = new MagazineMapper();
                destination.Item = magazineMapper.Map(source.Item as Magazine);
            }

            ReaderMapper readerMapper = new ReaderMapper();
            destination.Reader = readerMapper.Map(source.Reader);

            destination.Score = source.Score;

            return destination;
        }
    }
}