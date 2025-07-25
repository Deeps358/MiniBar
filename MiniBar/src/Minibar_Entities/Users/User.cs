namespace Minibar.Entities.Users
{
    public class User
    {
        public User(string userName, string passwordHash, string email, int roleId)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            RoleId = roleId;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
    }
}
