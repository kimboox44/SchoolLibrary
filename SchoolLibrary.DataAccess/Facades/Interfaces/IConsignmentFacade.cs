using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IConsignmentFacade
    {
        ConsignmentBusinessModel GetConsignmentById(int id);

        List<ConsignmentBusinessModel> GetAllConsignments();

        List<ConsignmentBusinessModel> GetAllConsignmentsByItemId(int itemId);

        void UpdateConsignment(ConsignmentBusinessModel consignment);

        ConsignmentBusinessModel CreateConsignment(int itemId, ConsignmentBusinessModel consignment);

        void WriteOffConsignmentById(int id);

        ConsignmentBusinessModel GetConsignmentByNumber(int number);
    }
}
