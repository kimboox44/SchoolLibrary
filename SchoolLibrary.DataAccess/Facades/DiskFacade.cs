namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class DiskFacade : IDiskFacade, IDisposable
    {
        private ILibraryUow uow;

        public DiskFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public DiskBusinessModel GetDiskById(int id)
        {
            var mapper = new DiskMapper();
            var item = this.uow.Items.GetById(id);
            var disk = item as Disk;
            if (disk != null)
            {
                return mapper.Map(disk);
            }

            return null;
        }

        public void UpdateDisk(DiskBusinessModel disk)
        {
            var mapper = new DiskMapper();

            var diskOld = this.uow.Items.GetById(disk.Id) as Disk;
            diskOld.Tags.Clear();

            var diskMapped = mapper.Map(disk);

            foreach (var tag in disk.Tags)
            {
                var t = this.uow.Tags.GetById(tag.id);
                diskOld.Tags.Add(t);
            }

            diskOld.Name = diskMapped.Name;
            diskOld.Producer = diskMapped.Producer;
            diskOld.Type = diskMapped.Type;
            diskOld.Year = diskMapped.Year;

            this.uow.Items.Update(diskOld);

            this.uow.Commit();
        }

        public void CreateDisk(DiskBusinessModel disk)
        {
            var mapper = new DiskMapper();
            var newDisk = mapper.Map(disk);
            this.uow.Items.Add(newDisk);
            this.uow.Commit();
        }

        public List<DiskBusinessModel> GetAllDisks()
        {
            var mapper = new DiskMapper();
            var disks = this.uow.Items.GetAll().OfType<Disk>().ToList();
            return disks.Select(mapper.Map).ToList();
        }

        public void RemoveById(int id)
        {
            this.uow.Items.Delete(id);
            this.uow.Commit();
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}
