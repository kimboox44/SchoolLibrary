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
    public class DiskMapper:IMapper<Disk,DiskBusinessModel>
    {
        public Disk Map(DiskBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            Disk disk = new Disk()
            {
                Id = source.Id,
                Name = source.Name,
                Producer = source.Producer,
                Type = source.Type,
                Year = source.Year,
                Tags = new Collection<Tag>()
            };
            disk.Tags=new Collection<Tag>();
            if (source.Tags != null)
            {
                TagMapper tagMapper=new TagMapper();
                foreach (var tag in source.Tags)
                {
                    disk.Tags.Add(tagMapper.Map(tag));
                }
            }
            return disk;
        }

        public DiskBusinessModel Map(Disk source)
        {
            if (source == null)
            {
                return null;
            }

            DiskBusinessModel disk = new DiskBusinessModel()
            {
                Id = source.Id,
                Name = source.Name,
                Producer = source.Producer,
                Type = source.Type,
                Year = source.Year,
                Tags = new Collection<TagBusinessModel>()
            };
            if (source.Tags != null)
            {
                TagMapper tagMapper=new TagMapper();
                foreach (var tag in source.Tags)
                {
                    disk.Tags.Add(tagMapper.Map(tag));
                }
            }
            return disk;
        }
    }
}
