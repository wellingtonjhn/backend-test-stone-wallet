using StoneWallet.Domain.Models.Entities;
using Xunit;

namespace StoneWallet.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void Should_CreateUser()
        {
            var user = new User("Wellington Nascimento", "wellington.jhn@gmail.com", "super_password");

            Assert.NotNull(user);
        }
    }
}
