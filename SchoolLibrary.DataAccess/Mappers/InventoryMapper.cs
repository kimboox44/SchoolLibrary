namespace SchoolLibrary.DataAccess.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class InventoryMapper : IMapper<Inventory, InventoryBusinessModel>
    {
        public Inventory Map(InventoryBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            Inventory inventory = new Inventory
            {
                InventoryId = source.InventoryId,
                Number = source.Number,
                IsAvailable = source.IsAvailable,
                WriteOffDate = source.WriteOffDate
            };

            var itemMapper = new ItemMapper();
            inventory.Item = itemMapper.Map(source.Item);

            return inventory;
        }

        public InventoryBusinessModel Map(Inventory source)
        {
            if (source == null)
            {
                return null;
            }

            InventoryBusinessModel inventoryBusinessModel = new InventoryBusinessModel
            {
                InventoryId = source.InventoryId,
                Number = source.Number,
                IsAvailable = source.IsAvailable,
                WriteOffDate = source.WriteOffDate
            };

            ConsignmentMapper consignmentMapper = new ConsignmentMapper();
            inventoryBusinessModel.Consignment = consignmentMapper.Map(source.Consignment);

            var itemMapper = new ItemMapper();
            inventoryBusinessModel.Item = itemMapper.Map(source.Item);

            return inventoryBusinessModel;
        }
    }
}
