using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Configuration
{
    using System.Configuration;

    public class Config
    {
        /// <summary>
        /// hide constructor
        /// </summary>
        public Config() { }

        /// <summary>
        /// name of configuration partition
        /// </summary>
        private const string CONFIGGROUPNAME = "SiteSettingsGroup/";

        /// <summary>
        /// name of section
        /// </summary>
        private const string CONFIGSECTIONNAME = "SiteSettings";

        /// <summary>
        /// reading whole configuration partition
        /// </summary>
        /// <returns></returns>
        public SiteSettings Get()
        {
            var config = (SiteSettings)ConfigurationManager
                    .GetSection(String.Concat(CONFIGGROUPNAME, CONFIGSECTIONNAME));
            return config;
        }
    }
}