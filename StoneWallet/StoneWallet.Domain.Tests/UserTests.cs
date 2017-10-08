using StoneWallet.Domain.Models.Entities;
using Xunit;

namespace StoneWallet.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void Should_CreateUser()
        {
            const string password = "super_password";
            var user = new User("Wellington Nascimento", "wellington.jhn@gmail.com", password);

            Assert.NotNull(user);
            Assert.NotEmpty(user.Name);
            Assert.NotEmpty(user.Email);
            Assert.NotEmpty(user.Password.Encoded);
            Assert.NotEqual(user.Password.Encoded, password);
        }
    }
}
