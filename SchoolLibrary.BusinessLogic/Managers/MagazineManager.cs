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
    public class MagazineManager:IMagazineManager,IDisposable
    {
        private IMagazineFacade magazineFacade;

        public MagazineManager(IMagazineFacade magazineFacade)
        {
            this.magazineFacade = magazineFacade;
        }

        public MagazineBusinessModel GetMagazineById(int id)
        {
            return this.magazineFacade.GetMagazineById(id);
        }

        public void UpdateMagazine(MagazineBusinessModel magazine)
        {
            this.magazineFacade.UpdateMagazine(magazine);
        }

        public void CreateMagazine(MagazineBusinessModel magazine)
        {
            this.magazineFacade.CreateMagazine(magazine);
        }

        public List<MagazineBusinessModel> GetAllMagazineBusinessModelss()
        {
            return this.magazineFacade.GetAllMagazines();
        }

        public void RemoveMagazineById(int id)
        {
            this.magazineFacade.RemoveMagazineById(id);
        }

        public void RemoveMagazine(MagazineBusinessModel magazine)
        {
            this.magazineFacade.RemoveMagazine(magazine);
        }
        
        public void Dispose()
        {
            if (this.magazineFacade as IDisposable != null)
            {
                (this.magazineFacade as IDisposable).Dispose();
            }
        }
    }

}
