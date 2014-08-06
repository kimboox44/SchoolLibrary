using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ConsignmentMapper : IMapper<Consignment, ConsignmentBusinessModel>
    {
        public ConsignmentBusinessModel Map(Consignment source)
        {
            if (source == null)
            {
                return null;
            }

            ConsignmentBusinessModel consignment = new ConsignmentBusinessModel
            {
                Id = source.Id,
                Number = source.Number,
                ArrivalDate = source.ArrivalDate,
                WriteOffDate = source.WriteOffDate
            };

            if (source.Inventories != null)
            {
                var inventoryMapper = new InventoryMapper();
                consignment.Inventories =
                    source.Inventories.Select(
                        a =>
                        inventoryMapper.Map(
                            new Inventory
                                {
                                    InventoryId = a.InventoryId,
                                    IsAvailable = a.IsAvailable,
                                    Number = a.Number,
                                    WriteOffDate = a.WriteOffDate
                                })).ToList();
            }
            if (source.Item != null)
            {
                var itemMapper = new ItemMapper();
                consignment.Item = itemMapper.Map(source.Item);
            }

            return consignment;
        }

        public Consignment Map(ConsignmentBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            Consignment consignment = new Consignment
            {
                Id = source.Id,
                ArrivalDate = source.ArrivalDate,
                WriteOffDate = source.WriteOffDate
            };

            if (source.Item != null)
            {
                var itemMapper = new ItemMapper();
                consignment.Item = itemMapper.Map(source.Item);
            }

            return consignment;
        }
    }
}
