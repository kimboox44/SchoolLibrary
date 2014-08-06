using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.MVCModels;

    public interface IExcelManager
    {
        ReadingFromExcelModel GetReadersFromFile(string fileName);

        string GenerateErrorString(ReadingFromExcelModel model);
    }
}
