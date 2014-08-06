namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public interface IMagazineFacade
    {
        MagazineBusinessModel GetMagazineById(int id);

        void UpdateMagazine(MagazineBusinessModel magazine);

        void CreateMagazine(MagazineBusinessModel magazine);

        List<MagazineBusinessModel> GetAllMagazines();

        void RemoveMagazineById(int id);

        MagazineBusinessModel GetMagazine(Magazine magazine);

        void RemoveMagazine(MagazineBusinessModel magazine);

        List<InventoryBusinessModel> GetAllInventories(int magazineId);
    }
}
