using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IDiskManager
    {
        DiskBusinessModel GetDiskById(int id);

        void UpdateDisk(DiskBusinessModel disk);

        void CreateDisk(DiskBusinessModel disk);

        List<DiskBusinessModel> GetAllDisks();

        void RemoveDiskById(int id);
    }
}
