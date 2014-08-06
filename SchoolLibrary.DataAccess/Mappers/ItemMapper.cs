using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ItemMapper:IMapper<Item,ItemBusinessModel>
    {
        public Item Map(ItemBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }
            Item destination;
            if (source is BookBusinessModel)
            {
                destination = new BookMapper().Map(source as BookBusinessModel);
            }
            else if (source is MagazineBusinessModel)
            {
                destination = new MagazineMapper().Map(source as MagazineBusinessModel);
            }
            else if (source is DiskBusinessModel)
            {
                destination = new DiskMapper().Map(source as DiskBusinessModel);
            }
            else
            {
                destination = null;
            }
            return destination;
        }

        public ItemBusinessModel Map(Item source)
        {
            if (source == null)
            {
                return null;
            }
            ItemBusinessModel destination;
            if (source is Book)
            {
                destination = new BookMapper().Map(source as Book);
            }
            else if (source is Magazine)
            {
                destination = new MagazineMapper().Map(source as Magazine);
            }
            else if (source is Disk)
            {
                destination = new DiskMapper().Map(source as Disk);
            }
            else
            {
                destination = null;
            }
            return destination;
        }
    }
}
