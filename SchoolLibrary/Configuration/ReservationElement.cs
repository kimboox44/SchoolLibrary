namespace SchoolLibrary.Configuration
{
    using System.Configuration;

    public class ReservationElement : ConfigurationElement
    {
        [ConfigurationProperty("DeleteAfterDays", DefaultValue = 5, IsRequired = true)]
        [IntegerValidator(MinValue = 3, MaxValue = 30, ExcludeRange = false)]
        public int DeleteAfterDays
        {
            get
            {
                return (int)this["DeleteAfterDays"];
            }
            set
            {
                this["DeleteAfterDays"] = value;
            }
        }

        [ConfigurationProperty("MaxReservedItems", DefaultValue = 3, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 10, ExcludeRange = false)]
        public int MaxReservedItems
        {
            get
            {
                return (int)this["MaxReservedItems"];
            }
            set
            {
                this["MaxReservedItems"] = value;
            }
        }

    }
}