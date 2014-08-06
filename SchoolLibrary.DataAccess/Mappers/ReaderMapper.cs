namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ReaderMapper : IMapper<Reader, ReaderBusinessModel>
    {
        public Reader Map(ReaderBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            UserProfileMapper userProfileMapper = new UserProfileMapper();
            TagMapper tagMapper = new TagMapper();

            ICollection<Tag> preferences = new List<Tag>();

            //foreach (var tag in source.Preferences)
            //{
            //    preferences.Add(tagMapper.Map(tag));
            //}

            Reader reader = new Reader
            {
                ReaderId = source.ReaderId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Address = source.Address,
                Birthday = source.Birthday,
                Phone = source.Phone,
                EMail = source.EMail,
                UserProfile = source.UserProfileBusiness != null ? userProfileMapper.Map(source.UserProfileBusiness) : null,
                Preferences = preferences
            };

            return reader;
        }

        public ReaderBusinessModel Map(Reader source)
        {
            if (source == null)
            {
                return null;
            }

            UserProfileMapper userProfileMapper = new UserProfileMapper();
            TagMapper tagMapper = new TagMapper();

            List<TagBusinessModel> preferences = new List<TagBusinessModel>();

            if (source.Preferences != null)
            {
                foreach (var tag in source.Preferences)
                {
                    preferences.Add(tagMapper.Map(tag));
                }
            }

            ReaderBusinessModel readerBusinessInfoModel = new ReaderBusinessModel
            {
                ReaderId = source.ReaderId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Address = source.Address,
                Birthday = source.Birthday,
                Phone = source.Phone,
                EMail = source.EMail,
                UserProfileBusiness = source.UserProfile != null ? userProfileMapper.Map(source.UserProfile) : null,
                Preferences = preferences
            };

            return readerBusinessInfoModel;
        }
    }
}