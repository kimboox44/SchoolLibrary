using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ConsignmentFacade : IConsignmentFacade, IDisposable
    {
        private ILibraryUow uow;

        public ConsignmentFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public ConsignmentBusinessModel GetConsignmentById(int id)
        {
            var mapper = new ConsignmentMapper();
            var consignment = this.uow.Consignments.GetById(id);
            return mapper.Map(consignment);
        }

        public List<ConsignmentBusinessModel> GetAllConsignmentsByItemId(int itemId)
        {
            var mapper = new ConsignmentMapper();
            var consignments = this.uow.Consignments.GetAll()
                                .Where(x => x.Item.Id == itemId).ToList()
                                .Select(mapper.Map).ToList();
            return consignments;
        }

        public List<ConsignmentBusinessModel> GetAllConsignments()
        {
            var mapper = new ConsignmentMapper();
            var consignments = this.uow.Consignments.GetAll().ToList().Select(mapper.Map).ToList();
            return consignments;
        }

        public void UpdateConsignment(ConsignmentBusinessModel consignment)
        {
            var mapper = new ConsignmentMapper();
            this.uow.Consignments.Update(mapper.Map(consignment));
            this.uow.Commit();
        }

        public ConsignmentBusinessModel CreateConsignment(int itemId, ConsignmentBusinessModel consignment)
        {
            ConsignmentBusinessModel consNew;
            var mapper = new ConsignmentMapper();

            int id = 0;
            using (var uowNew = new LibraryUow())
            {
                var item = uowNew.Items.GetById(itemId);
                var entryConsignment = mapper.Map(consignment);
                entryConsignment.Item = item;
                uowNew.Consignments.Add(entryConsignment);

                uowNew.Commit();
                id = entryConsignment.Id;
            }

            consNew = mapper.Map(this.uow.Consignments.GetById(id));
            consignment.Id = consNew.Id;
            consignment.Number = consNew.Number;
            return consNew;
        }

        public void WriteOffConsignmentById(int id)
        {
            var consignment = this.uow.Consignments.GetById(id);
            consignment.WriteOffDate = DateTime.Now;
            foreach (var inventory in consignment.Inventories)
            {
                inventory.IsAvailable = false;
                inventory.WriteOffDate = DateTime.Now;
            }

            this.uow.Consignments.Update(consignment);
            this.uow.Commit();
        }

        public ConsignmentBusinessModel GetConsignmentByNumber(int number)
        {
            var consignment = this.uow.Consignments.GetAll().SingleOrDefault(c => c.Number == number);
            var mapper = new ConsignmentMapper();
            return mapper.Map(consignment);

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
