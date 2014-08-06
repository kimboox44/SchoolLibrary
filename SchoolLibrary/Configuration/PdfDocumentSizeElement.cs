using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Configuration
{
    using System.Configuration;
    public class PdfDocumentSizeElement : ConfigurationElement
    {
        [ConfigurationProperty("Width", DefaultValue = "3", IsRequired = true)]
        public float Width
        {
            get
            {
                return (float)this["Width"];
            }
            set
            {
                this["Width"] = value;
            }
        }

        [ConfigurationProperty("Height", DefaultValue = "1", IsRequired = true)]
        public float Height
        {
            get
            {
                return (float)this["Height"];
            }
            set
            {
                this["Height"] = value;
            }
        }
        [ConfigurationProperty("VMargin", DefaultValue = "10", IsRequired = true)]
        public float VMargin
        {
            get
            {
                return (float)this["VMargin"];
            }
            set
            {
                this["VMargin"] = value;
            }
        }

        [ConfigurationProperty("HMargin", DefaultValue = "10", IsRequired = true)]
        public float HMargin
        {
            get
            {
                return (float)this["HMargin"];
            }
            set
            {
                this["HMargin"] = value;
            }
        }
        [ConfigurationProperty("Dpi", DefaultValue = "72", IsRequired = true)]
        public float Dpi
        {
            get
            {
                return (float)this["Dpi"];
            }
            set
            {
                this["Dpi"] = value;
            }
        }
        [ConfigurationProperty("FontSize", DefaultValue = "11", IsRequired = true)]
        public float FontSize
        {
            get
            {
                return (float)this["FontSize"];
            }
            set
            {
                this["FontSize"] = value;
            }
        }
    }
}