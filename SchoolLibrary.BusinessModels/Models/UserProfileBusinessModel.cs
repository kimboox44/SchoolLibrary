namespace SchoolLibrary.BusinessModels.Models
{
    using System;

    public class UserProfileBusinessModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public DateTime CreationDate { get; set; }

        public string Email { get; set; }

        public static bool operator ==(UserProfileBusinessModel u1, UserProfileBusinessModel u2)
        {
            if (System.Object.ReferenceEquals(u1, u2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)u1 == null) || ((object)u2 == null))
            {
                return false;
            }

            return u1.UserId == u2.UserId && u1.Email == u2.Email && u1.CreationDate == u2.CreationDate && u1.Role == u2.Role && u1.UserName == u2.UserName;
        }

        public static bool operator !=(UserProfileBusinessModel u1, UserProfileBusinessModel u2)
        {
            return !(u1 == u2);
        }

    }
}