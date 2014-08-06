namespace SchoolLibrary.DataAccess.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        TSource Map(TDestination source);

        TDestination Map(TSource source);

        //ICollection<TDestination> Map(ICollection<TSource> sources);

        //ICollection<TSource> Map(ICollection<TDestination> sources);
    }
}