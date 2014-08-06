using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.Models;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IMagazineManager
    {
        MagazineBusinessModel GetMagazineById(int id);

       void CreateMagazine(MagazineBusinessModel magazine);

        List<MagazineBusinessModel> GetAllMagazineBusinessModelss();

        void UpdateMagazine(MagazineBusinessModel magazine);

        void RemoveMagazineById(int id);

        void RemoveMagazine(MagazineBusinessModel magazine);
    }
}
