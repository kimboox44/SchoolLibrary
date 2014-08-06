using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System.IO;

    using SchoolLibrary.BusinessModels.Models;

    public interface IConsignmentManager
    {
        ConsignmentBusinessModel GetConsignmentById(int id);

        List<ConsignmentBusinessModel> GetAllConsignments();

        List<ConsignmentBusinessModel> GetAllConsignmentsByItemId(int itemId);

        ConsignmentBusinessModel GetConsignmentByNumber(int number);

        void UpdateConsignment(ConsignmentBusinessModel consignment);

        ConsignmentBusinessModel CreateConsignment(int itemId, ConsignmentBusinessModel consignment);

        void WriteOffConsignmentById(int id);

        List<ConsignmentsForGrid> GetConsignmentForFiltering(int skip, int take, out int filteredCount,
            List<Func<ConsignmentBusinessModel, bool>> filters = null);

        List<ConsignmentsForGrid> GetConsignmentForFiltering(int itemId, int skip, int take, out int filteredCount,
            List<Func<ConsignmentBusinessModel, bool>> filters = null);

        MemoryStream GetPdfByConsignmentNumber(int number);

    }
}
