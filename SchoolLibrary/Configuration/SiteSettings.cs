namespace SchoolLibrary.Configuration
{
    using System.Configuration;

    public class SiteSettings : ConfigurationSection
    {
        [ConfigurationProperty("Reservation", IsRequired = true)]
        public ReservationElement Reserervation
        {
            get
            {
                return (ReservationElement)this["Reservation"];
            }
            set
            {
                this["Reservation"] = value;
            }
        }
        [ConfigurationProperty("InventoryNumber", IsRequired = true)]
        public InventoryNumberElement InventoryNumber
        {
            get
            {
                return (InventoryNumberElement)this["InventoryNumber"];
            }
            set
            {
                this["InventoryNumber"] = value;
            }
        }
        [ConfigurationProperty("PdfDocumentSize", IsRequired = true)]
        public PdfDocumentSizeElement PdfDocumentSize
        {
            get
            {
                return (PdfDocumentSizeElement)this["PdfDocumentSize"];
            }
            set
            {
                this["PdfDocumentSize"] = value;
            }
        }
    }
}