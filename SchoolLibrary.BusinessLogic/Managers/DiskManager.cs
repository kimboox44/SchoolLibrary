using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Facades.Interfaces;

namespace SchoolLibrary.BusinessLogic.Managers
{
    public class DiskManager : IDiskManager,IDisposable
    {
        private IDiskFacade diskFacade;

        public DiskManager(IDiskFacade diskFacade)
        {
            this.diskFacade = diskFacade;
        }

        public DiskBusinessModel GetDiskById(int id)
        {
            return this.diskFacade.GetDiskById(id);
        }

        public void UpdateDisk(DiskBusinessModel disk)
        {
            this.diskFacade.UpdateDisk(disk);
        }

        public void CreateDisk(DiskBusinessModel disk)
        {
            this.diskFacade.CreateDisk(disk);
        }

        public List<DiskBusinessModel> GetAllDisks()
        {
            return this.diskFacade.GetAllDisks();
        }

        public void RemoveDiskById(int id)
        {
            this.diskFacade.RemoveById(id);
        }
        
        public void Dispose()
        {
            if (this.diskFacade as IDisposable != null)
            {
                (this.diskFacade as IDisposable).Dispose();
            }
        }
    }

}
