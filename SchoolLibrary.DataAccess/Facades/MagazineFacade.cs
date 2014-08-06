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

    public class MagazineFacade : IMagazineFacade, IDisposable
    {
        private ILibraryUow uow;

        public MagazineFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public MagazineBusinessModel GetMagazineById(int id)
        {
            Magazine magazine = this.uow.Items.GetById(id) as Magazine;
            return new MagazineMapper().Map(magazine);
        }

        public void UpdateMagazine(MagazineBusinessModel magazineBusinessModel)
        {
            var mapper = new MagazineMapper();

            var magazineOld = this.uow.Items.GetById(magazineBusinessModel.Id) as Magazine;
           
            magazineOld.Tags.Clear();

            var magazineMapped = mapper.Map(magazineBusinessModel);

            foreach (var tag in magazineBusinessModel.Tags)
            {
                var t = this.uow.Tags.GetById(tag.id);
                magazineOld.Tags.Add(t);
            }

            magazineOld.Name = magazineMapped.Name;
            magazineOld.Issue = magazineMapped.Issue;
            magazineOld.PageCount = magazineMapped.PageCount;
            magazineOld.Publisher = magazineMapped.Publisher;
            magazineOld.Year = magazineMapped.Year;

            this.uow.Items.Update(magazineOld);

            this.uow.Commit();
        }

        public void CreateMagazine(MagazineBusinessModel magazine)
        {
            this.uow.Items.Add(new MagazineMapper().Map(magazine));
            this.uow.Commit();
        }

        public List<MagazineBusinessModel> GetAllMagazines()
        {
            MagazineMapper mapper = new MagazineMapper();
            List<Magazine> list = this.uow.Items.GetAll().OfType<Magazine>().ToList();
            List<MagazineBusinessModel> listBusinessModels = new List<MagazineBusinessModel>();
            foreach (Magazine magazine in list)
            {
                listBusinessModels.Add(mapper.Map(magazine));
            }

            return listBusinessModels;
        }

        public void RemoveMagazineById(int id)
        {
            this.uow.Items.Delete(id);
        }

        public MagazineBusinessModel GetMagazine(Magazine magazine)
        {
            return new MagazineMapper().Map(this.uow.Items.GetById(magazine.Id) as Magazine);
        }

        public void RemoveMagazine(MagazineBusinessModel magazine)
        {
            this.uow.Items.Delete(magazine.Id);
        }

        public List<InventoryBusinessModel> GetAllInventories(int magazineId)
        {
            List<Inventory> list = this.uow.Items.GetById(magazineId).Inventories.ToList();
            List<InventoryBusinessModel> listBusinessModel = new List<InventoryBusinessModel>();
            foreach (Inventory inventory in list)
            {
                listBusinessModel.Add(new InventoryMapper().Map(inventory));
            }

            return listBusinessModel;
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
