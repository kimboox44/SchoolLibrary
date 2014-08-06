using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SchoolLibrary.Services
{
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;

    using StructureMap;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReaderManagment" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReaderManagment.svc or ReaderManagment.svc.cs at the Solution Explorer and start debugging.
    public class ReaderManagment : IReaderManagment
    {
        private IReaderManager readerManager;

        public ReaderManagment()
        {
            this.readerManager = ObjectFactory.GetInstance<IReaderManager>();
        }

        public List<ReaderBusinessModel> GetAllReaders()
        {
            return this.readerManager.GetAllReaders();
        }

        public void UpdateReader(ReaderBusinessModel reader)
        {
            this.readerManager.UpdateReader(reader);
        }
    }
}
