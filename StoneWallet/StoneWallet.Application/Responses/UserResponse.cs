namespace StoneWallet.Application.Responses
{
    public class UserResponse
    {
        public string Id { get; }
        public string Email { get; }
        public string Name { get; }

        public UserResponse(string id, string email, string name)
        {
            Id = id;
            Email = email;
            Name = name;
        }
    }
}