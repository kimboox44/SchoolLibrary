using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.MVCModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The author short info.
    /// !!! Fields must be named as "id" and "name" for jquery-tokeninput
    /// </summary>
    public class AuthorShortInfo
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}
