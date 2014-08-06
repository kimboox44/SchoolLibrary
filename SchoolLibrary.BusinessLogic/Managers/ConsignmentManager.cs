using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.IO;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessLogic.Other;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ConsignmentManager : IConsignmentManager, IDisposable
    {
        private IConsignmentFacade consignmentFacade;
        
        public ConsignmentManager(IConsignmentFacade consignmentFacade)
        {
            this.consignmentFacade = consignmentFacade;
        }    

        public ConsignmentBusinessModel GetConsignmentById(int id)
        {
            return this.consignmentFacade.GetConsignmentById(id);
        }

        public List<ConsignmentBusinessModel> GetAllConsignments()
        {
            return this.consignmentFacade.GetAllConsignments();
        }

        public List<ConsignmentBusinessModel> GetAllConsignmentsByItemId(int itemId)
        {
            return this.consignmentFacade.GetAllConsignmentsByItemId(itemId);
        }

        public ConsignmentBusinessModel GetConsignmentByNumber(int number)
        {
            return this.consignmentFacade.GetConsignmentByNumber(number);
        }

        public void UpdateConsignment(ConsignmentBusinessModel consignment)
        {
            this.consignmentFacade.UpdateConsignment(consignment);
        }

        public ConsignmentBusinessModel CreateConsignment(int itemId, ConsignmentBusinessModel consignment)
        {
            return this.consignmentFacade.CreateConsignment(itemId,consignment);
        }

        public void WriteOffConsignmentById(int id)
        {
            this.consignmentFacade.WriteOffConsignmentById(id);
        }

        public List<ConsignmentsForGrid> GetConsignmentForFiltering(int skip, int take, out int filteredCount,
            List<Func<ConsignmentBusinessModel, bool>> filters = null)
        {
            var consignments = this.GetAllConsignments().AsQueryable();

            if (filters != null)
            {
                foreach (var predicate in filters)
                {
                    consignments = consignments.Where(predicate).AsQueryable();
                }
            }

            filteredCount = consignments.Count();

            return consignments.Skip(skip).Take(take).Select(c=>new ConsignmentsForGrid(c)).ToList();
        }

        public List<ConsignmentsForGrid> GetConsignmentForFiltering(int itemId, int skip, int take, out int filteredCount,
            List<Func<ConsignmentBusinessModel, bool>> filters = null)
        {
            var consignments = this.GetAllConsignmentsByItemId(itemId).AsQueryable();
            if (filters != null)
            {
                foreach (var predicate in filters)
                {
                    consignments = consignments.Where(predicate).AsQueryable();
                }
            }

            filteredCount = consignments.Count();

            return consignments.Skip(skip).Take(take).Select(c => new ConsignmentsForGrid(c)).ToList();
        }

        public MemoryStream GetPdfByConsignmentNumber(int number)
        {
            var consignment = this.consignmentFacade.GetConsignmentByNumber(number);
            if (consignment == null)
            {
                return null;
            }

            var pdfGenerator = new PdfGenerator();
            pdfGenerator.PdfInit(consignment.Item, consignment);
            foreach (var inv in consignment.Inventories)
            {
                pdfGenerator.BarCodeGenerate(inv.Number);
            }

            return pdfGenerator.PdfFinish();
        }

        public void Dispose()
        {
            if (this.consignmentFacade as IDisposable != null)
            {
                (this.consignmentFacade as IDisposable).Dispose();
            }
        }

    }
}
