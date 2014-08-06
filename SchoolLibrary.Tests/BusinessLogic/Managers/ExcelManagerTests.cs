using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.BusinessLogic.Managers
{
    using SchoolLibrary.BusinessLogic.Managers;

    [TestClass]
    public class ExcelManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Invalid input file structure!")]
        public void GetReadersFromFile_ArgumentException()
        {
            var manager = new ExcelManager();
            manager.GetReadersFromFile(string.Empty);
        }
    }
}
