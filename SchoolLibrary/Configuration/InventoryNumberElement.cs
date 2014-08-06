
namespace SchoolLibrary.Configuration
{
    using System.Configuration;
    public class InventoryNumberElement : ConfigurationElement
    {
        [ConfigurationProperty("ConsignmentFormat", DefaultValue = 10, IsRequired = true)]
        [IntegerValidator(MinValue = 3, MaxValue = 30, ExcludeRange = false)]
        public int ConsignmentFormat
        {
            get
            {
                return (int)this["ConsignmentFormat"];
            }
            set
            {
                this["ConsignmentFormat"] = value;
            }
        }

        [ConfigurationProperty("InventoryFormat", DefaultValue = 4, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 10, ExcludeRange = false)]
        public int InventoryFormat
        {
            get
            {
                return (int)this["InventoryFormat"];
            }
            set
            {
                this["InventoryFormat"] = value;
            }
        }
    }
}