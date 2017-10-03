using StoneWallet.Domain.Models.Entities;
using System;
using System.Linq;
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
        public void Should_AddMoreThanOneCreditCard()
        {
            // Arrange
            var user = GetValidUser();
            var creditCardOne = new CreditCard("Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var creditCardTwo = new CreditCard("Katia", 987654321, 124563, 500, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            var wallet = new Wallet(user);

            // Act
            wallet.AddCreditCard(creditCardOne);
            wallet.AddCreditCard(creditCardTwo);

            // Assert
            Assert.Equal(2, wallet.CreditCards.Count);
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
        public void Should_RemoveCreditCard_When_ExistsInWallet()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var wallet = new Wallet(user);
            wallet.AddCreditCard(creditCard);
            
            // Act
            wallet.RemoveCreditCard(creditCard.Number);

            // Assert
            Assert.Empty(wallet.CreditCards);
        }

        [Fact]
        public void Cannot_RemoveCreditCard_When_NotExistsInWallet()
        {
            // Arrange
            var user = GetValidUser();
            var creditCard = GetValidCreditCard();
            var wallet = new Wallet(user);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => wallet.RemoveCreditCard(creditCard.Number));
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

        [Fact]
        public void Should_CannotMakeAPurchase_When_WalletNotContainsAnyCreditCard()
        {
            // Arrange
            var user = GetValidUser();
            var wallet = new Wallet(user);

            // Act
            Assert.Throws<InvalidOperationException>(() => wallet.Buy(500));
        }

        [Fact]
        public void Should_MakeAPurchase_With_HigherDueDateCreditCard()
        {
            // Arrange
            var user = GetValidUser();
            var wallet = new Wallet(user);
            var higherDueDateCreditCard = new CreditCard("Katia", 987654321, 789, 500, DateTime.Now.AddDays(60), DateTime.Now.AddYears(1));
            var anotherCreditCard = new CreditCard("Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(higherDueDateCreditCard);
            wallet.AddCreditCard(anotherCreditCard);

            // Act
            wallet.Buy(300);

            // Assert
            var selectedCreditCard = wallet.CreditCards.First(a => a.Number == higherDueDateCreditCard.Number);
            Assert.Equal(200, selectedCreditCard.AvailableCredit);
            Assert.Equal(1200, wallet.AvailableCredit);
        }

        [Fact]
        public void Should_MakeAPurchase_With_MinimumLimitCreditCard()
        {
            // Arrange
            var user = GetValidUser();
            var wallet = new Wallet(user);
            var minimumLimitCreditCard = new CreditCard("Wellington", 987654321, 789, 500, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var anotherCreditCard = new CreditCard("Katia", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(minimumLimitCreditCard);
            wallet.AddCreditCard(anotherCreditCard);

            // Act
            wallet.Buy(175);

            // Assert
            var selectedCreditCard = wallet.CreditCards.First(a => a.Number == minimumLimitCreditCard.Number);
            Assert.Equal(325, selectedCreditCard.AvailableCredit);
            Assert.Equal(1325, wallet.AvailableCredit);
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
