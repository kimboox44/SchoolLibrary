using System;
using System.Collections.Generic;


namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.Linq;
    using System.Transactions;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.DataAccess.Facades.Interfaces;

    public class ReaderManager : IReaderManager, IDisposable
    {
        private ILibraryUow uow;

        private IReaderFacade readerFacade;

        private IUsersFacade usersFacade;

        public ReaderManager(ILibraryUow uow, IReaderFacade readerFacade, IUsersFacade usersFacade)
        {
            this.uow = uow;
            this.readerFacade = readerFacade;
            this.usersFacade = usersFacade;
        }

        public ReadersGridModel GetReadersForGrid(IEnumerable<KeyValuePair<string,string>> query, 
            int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var readers = this.GetAllReaders();
            var totalRows = readers.Count;
            var filtersCount = 0;

            if (query != null)
            {
                filtersCount = int.Parse(query.Where(r => r.Key == "filterscount").Select(r => r.Value).First());
                for (var i = 0; i < filtersCount; i++)
                {
                    var filterValue = query.Where(r => r.Key == "filtervalue" + i).Select(r => r.Value).First();
                    var filterDataField = query.Where(r => r.Key == "filterdatafield" + i).Select(r => r.Value).First();
                    readers =
                        readers.Where(
                            r =>
                            r.GetType()
                             .GetProperty(filterDataField)
                             .GetValue(r)
                             .ToString()
                             .ToLower()
                             .Contains(filterValue.ToLower())).Select(r => r).ToList();
                }
            }

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    readers = readers.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }
                else
                {
                    readers = readers.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }
            }

            readers = readers.Skip(pageNum * pageSize).Take(pageSize).ToList();
            var model = new ReadersGridModel();
            if (filtersCount > 0)
            {
                totalRows = readers.Count();
            }

            model.TotalRows = totalRows;
            model.Readers = readers;
            return model;
        }

        public ReaderBusinessModel GetReaderById(int id)
        {
            return this.readerFacade.GetReaderById(id);
        }

        public ReaderBusinessModel GetReaderByUserId(int id)
        {
            return this.readerFacade.GetReaderByUserId(id);
        }

        public void UnbindReaderAndUser(int userId)
        {
            var reader = this.GetReaderByUserId(userId);
            if (reader != null)
            {
                reader.UserProfileBusiness = null;
                var mapper = new ReaderMapper();
                this.uow.Readers.Update(mapper.Map(reader));
                this.uow.Commit();
            }
        }

        public void UpdateReader(ReaderBusinessModel reader)
        {
            var currentReader = this.GetReaderById(reader.ReaderId);
            if (reader.UserProfileBusiness != null && currentReader.EMail != reader.EMail)
            {
                using (var scope = new TransactionScope())
                {
                    this.readerFacade.UpdateReader(reader);
                    this.usersFacade.UpdateUsersEmail(reader.UserProfileBusiness.UserId, reader.EMail);

                    scope.Complete();
                }
            }
            else
            {
                this.readerFacade.UpdateReader(reader);
            }
        }

        public void CreateReader(ReaderBusinessModel reader)
        {
            if (!this.IsExisting(reader))
            {
                this.readerFacade.CreateReader(reader);
            }
        }

        public List<ReaderBusinessModel> CreateReaders(IEnumerable<ReaderBusinessModel> readers)
        {
            var addedReaders = new List<ReaderBusinessModel>();
            foreach (var reader in readers)
            {
                if (!this.IsExisting(reader))
                {
                    this.readerFacade.CreateReader(reader);
                    addedReaders.Add(reader);
                }
            }

            return addedReaders;
        }

        private bool IsExisting(ReaderBusinessModel reader)
        {
            return
                this.GetAllReaders()
                    .Any(
                        r =>
                        r.FirstName == reader.FirstName && r.LastName == reader.LastName
                        && r.Birthday == reader.Birthday);
        }

        public ReaderBusinessModel GetReaderByEmail(string email)
        {
            return this.readerFacade.GetReaderByEmail(email);
        }

        public ReaderBusinessModel GetReaderByFullName(string fullName)
        {
            string[] array = fullName.Split(' ');
            string firstName = array[0];
            string lastName = array[1];
            return this.readerFacade.GetReaderByFullName(firstName,lastName);
        }

        public List<ReaderBusinessModel> GetAllReaders()
        {
            return this.readerFacade.GetAllReaders();
        }

        public List<ReaderBusinessModel> GetReadersFromRange(int skip, int take)
        {
            var allReaders = this.GetAllReaders();
            return allReaders.Skip(skip).Take(take).ToList();
        }

        public void RemoveReaderById(int id)
        {
            this.readerFacade.RemoveReaderById(id);
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }

            if (this.readerFacade as IDisposable != null)
            {
                (this.readerFacade as IDisposable).Dispose();
            }
    }
    }
}
