using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ReaderFacade : IReaderFacade, IDisposable
    {
        private ILibraryUow uow;

        public ReaderFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public ReaderBusinessModel GetReaderById(int id)
        {
            ReaderMapper mapper = new ReaderMapper();
            var reader = mapper.Map(this.uow.Readers.GetById(id));
            return reader;
        }

        public ReaderBusinessModel GetReaderByUserId(int id)
        {
            ReaderMapper mapper = new ReaderMapper();
            var reader = this.uow.Readers.GetAll().FirstOrDefault(r => r.UserProfile.UserId == id);
            return mapper.Map(reader);
        }

        public void UpdateReader(ReaderBusinessModel reader)
        {
            List<Tag> preferences = new List<Tag>();
            if (reader.Preferences != null)
            {
                foreach (var tag in reader.Preferences)
                {
                    preferences.Add(this.uow.Tags.GetById(tag.id));
                }
            }

            Reader currentReader = uow.Readers.GetById(reader.ReaderId);
            if (reader.UserProfileBusiness != null)
            {
                UserProfile user = this.uow.UsersProfiles.GetById(reader.UserProfileBusiness.UserId);
                currentReader.UserProfile = user;
            }
            currentReader.Address = reader.Address;
            currentReader.Birthday = reader.Birthday;
            currentReader.EMail = reader.EMail;
            currentReader.FirstName = reader.FirstName;
            currentReader.LastName = reader.LastName;
            currentReader.Phone = reader.Phone;
            currentReader.Preferences = preferences;
            uow.Readers.Update(currentReader);
            uow.Commit();
        }

        public void CreateReader(ReaderBusinessModel reader)
        {
            var mapper = new ReaderMapper();
            this.uow.Readers.Add(mapper.Map(reader));
            this.uow.Commit();
        }

        public ReaderBusinessModel GetReaderByEmail(string email)
        {
            ReaderBusinessModel readerBusinessModel = new ReaderBusinessModel();
            Reader reader = this.uow.Readers.GetAll().FirstOrDefault(r => r.EMail == email);
            ReaderMapper readerMapper = new ReaderMapper();
            readerBusinessModel = readerMapper.Map(reader);
            return readerBusinessModel;
        }

        public ReaderBusinessModel GetReaderByFullName(string firstName,string lastName)
        {
            ReaderBusinessModel readerBusiness=new ReaderBusinessModel();
            Reader reader =
                this.uow.Readers.GetAll().FirstOrDefault(r => (r.FirstName == firstName && r.LastName == lastName));
            ReaderMapper mapper=new ReaderMapper();
            readerBusiness = mapper.Map(reader);
            return readerBusiness;
        }

        public List<ReaderBusinessModel> GetAllReaders()
        {
            ReaderMapper mapper = new ReaderMapper();
            var readers = new List<ReaderBusinessModel>();
            readers = this.uow.Readers.GetAll().ToList().Select(mapper.Map).ToList();

            return readers;
        }

        public void RemoveReaderById(int id)
        {
            this.uow.Readers.Delete(id);
            this.uow.Commit();
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}