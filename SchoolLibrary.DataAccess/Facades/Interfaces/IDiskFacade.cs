namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Mappers;

    public interface IDiskFacade
    {
        DiskBusinessModel GetDiskById(int id);

        void UpdateDisk(DiskBusinessModel disk);

        void CreateDisk(DiskBusinessModel disk);

        List<DiskBusinessModel> GetAllDisks();

        void RemoveById(int id);
    }
}
