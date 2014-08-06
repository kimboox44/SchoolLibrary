using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Entities;

namespace SchoolLibrary.DataAccess.Mappers
{
    public class MagazineMapper:IMapper<Magazine,MagazineBusinessModel>
    {
        public Magazine Map(MagazineBusinessModel source)
        {
            if (source == null)
                return null;
            Magazine magazine = new Magazine()
            {
                Id = source.Id,
                Issue = source.Issue,
                Name = source.Name,
                PageCount = source.PageCount,
                Publisher = source.Publisher,
                Year = source.Year
            };
            magazine.Tags=new Collection<Tag>();
            if (source.Tags != null)
            {
                TagMapper tagMapper=new TagMapper();
                foreach (var tag in source.Tags)
                {
                    magazine.Tags.Add(tagMapper.Map(tag));
                }
            }
            return magazine;
        }

        public MagazineBusinessModel Map(Magazine source)
        {
            if (source == null)
            {
                return null;
            }

            MagazineBusinessModel magazine = new MagazineBusinessModel()
            {
                Id = source.Id,
                Issue = source.Issue,
                Name = source.Name,
                PageCount = source.PageCount,
                Publisher = source.Publisher,
                Year = source.Year
            };
            if (source.Tags != null)
            {
                TagMapper tagMapper = new TagMapper();
                foreach (var tag in source.Tags)
                {
                    magazine.Tags.Add(tagMapper.Map(tag));
                }
            }
            return magazine;
        }
    }
}
