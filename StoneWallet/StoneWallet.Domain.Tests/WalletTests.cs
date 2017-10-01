using StoneWallet.Domain.Models.Entities;
using System;
using Xunit;

namespace StoneWallet.Domain.Tests
{
    public class WalletTests
    {
        [Fact]
        public void Should_CreateWallet()
        {
            // Arrange
            var user = GetValidUser();

            // Act
            var wallet = new Wallet(user);

            // Assert
            Assert.NotNull(wallet);
            Assert.NotNull(wallet.User);
        }

        [Fact]
        public void Should_AddCreditCard_When_NotExists()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var wallet = new Wallet(user);

            // Act
            wallet.AddCreditCard(creditCard);

            // Assert
            Assert.Contains(creditCard, wallet.CreditCards);
        }

        [Fact]
        public void Cannot_AddCreditCard_When_ExistsCreditCardWithSameNumber()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var wallet = new Wallet(user);
            wallet.AddCreditCard(creditCard);

            // Act and Assert
            var anotherCreditCard = GetValidCreditCard();
            Assert.Throws<InvalidOperationException>(() => wallet.AddCreditCard(anotherCreditCard));
        }

        [Fact]
        public void Should_ChangeWalletLimit_When_NotExceedsMaxLimit()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var newLimit = 1000;

            var wallet = new Wallet(user);
            wallet.AddCreditCard(creditCard);

            // Act
            wallet.ChangeWalletLimit(newLimit);

            // Assert
            Assert.Equal(newLimit, wallet.WalletLimit);
        }

        [Fact]
        public void Cannot_ChangetWalletLimit_When_ExceedsMaxLimit()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var newLimit = 2000;

            var wallet = new Wallet(user);
            wallet.AddCreditCard(creditCard);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => wallet.ChangeWalletLimit(newLimit));
        }

        [Fact]
        public void Should_WalletAvailableLimit_EqualsTo_AvailableSumCreditCardLimit()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var newLimit = 1000;

            var wallet = new Wallet(user);
            wallet.AddCreditCard(creditCard);

            // Act
            wallet.ChangeWalletLimit(newLimit);

            // Assert
            Assert.Equal(newLimit, wallet.AvailableCredit);
        }

        private static User GetValidUser()
        {
            return new User("Wellington Nascimento", "wellington.jhn@gmail.com", "super_password");
        }

        private static CreditCard GetValidCreditCard()
        {
            return new CreditCard("Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
        }
    }
}
