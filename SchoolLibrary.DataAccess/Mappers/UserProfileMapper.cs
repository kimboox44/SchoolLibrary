namespace SchoolLibrary.DataAccess.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class UserProfileMapper : IMapper<UserProfile, UserProfileBusinessModel>
    {
        public UserProfile Map(UserProfileBusinessModel source)
        {
            ReaderMapper readerMapper=new ReaderMapper();
            if (source == null) return null;

            UserProfile userProfile = new UserProfile
            {
                UserId = source.UserId,
                UserName = source.UserName,
                Email = source.Email
            };

            return userProfile;
        }


        public UserProfileBusinessModel Map(UserProfile source)
        {
            ReaderMapper readerMapper=new ReaderMapper();
            if (source == null) return null;

            UserProfileBusinessModel userProfileBusinessViewModel = new UserProfileBusinessModel
            {
                UserId = source.UserId,
                UserName = source.UserName,
                Email = source.Email
            };
            return userProfileBusinessViewModel;
        }
    }
}
