namespace StoneWallet.Domain.Models.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; }
        public string Username { get; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public User(string username, string password, string name, string email)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

    }
}