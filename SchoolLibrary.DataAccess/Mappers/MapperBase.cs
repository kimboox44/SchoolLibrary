namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The MapperBase abstract class implements mapping for ICollection.
    /// Under Construction. Don't use!
    /// </summary>
    /// <typeparam name="TSource">
    /// </typeparam>
    /// <typeparam name="TDestination">
    /// </typeparam>
    public abstract class MapperBase<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        public abstract TDestination Map(TSource source);

        public abstract TSource Map(TDestination source);

        public ICollection<TDestination> Map(ICollection<TSource> sources)
        {
            return (ICollection<TDestination>)(sources == null ? new List<TDestination>() : sources.Select(this.Map));
        }

        public ICollection<TSource> Map(ICollection<TDestination> sources)
        {
            return (ICollection<TSource>)(sources == null ? new List<TSource>() : sources.Select(this.Map));
        }
    }
}
