using System;
namespace FloorballPCL.Model
{
    public enum UserRole
    {
        Admin, User
    }

    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

    }
}
