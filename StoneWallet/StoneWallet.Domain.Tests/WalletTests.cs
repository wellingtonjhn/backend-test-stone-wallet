﻿using StoneWallet.Domain.Models.Entities;
using System;
using System.Linq;
using Xunit;

namespace StoneWallet.Domain.Tests
{
    public class WalletTests
    {
        private Guid UserId { get; }

        public WalletTests()
        {
            UserId = Guid.NewGuid();
        }

        [Fact]
        public void Should_CreateWallet()
        {
            // Act
            var wallet = new Wallet(UserId);

            // Assert
            Assert.NotNull(wallet);
        }

        [Fact]
        public void Should_AddCreditCard_When_NotExists()
        {
            // Arrange
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var wallet = new Wallet(UserId);

            // Act
            wallet.AddCreditCard(creditCard);

            // Assert
            Assert.Contains(creditCard, wallet.CreditCards);
        }

        [Fact]
        public void Should_AddMoreThanOneCreditCard()
        {
            // Arrange
            var creditCardOne = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var creditCardTwo = new CreditCard(Guid.NewGuid(), "Katia", 987654321, 124563, 500, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            var wallet = new Wallet(UserId);

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
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var wallet = new Wallet(UserId);
            wallet.AddCreditCard(creditCard);

            // Act and Assert
            var anotherCreditCard = new CreditCard(Guid.NewGuid(), "Katia", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            Assert.Throws<InvalidOperationException>(() => wallet.AddCreditCard(anotherCreditCard));
        }

        [Fact]
        public void Should_RemoveCreditCard_When_ExistsInWallet()
        {
            // Arrange
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var wallet = new Wallet(UserId);
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
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var wallet = new Wallet(UserId);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => wallet.RemoveCreditCard(creditCard.Number));
        }

        [Fact]
        public void Should_ChangeWalletLimit_When_NotExceedsMaxLimit()
        {
            // Arrange
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var newLimit = 1000;

            var wallet = new Wallet(UserId);
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
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var newLimit = 2000;

            var wallet = new Wallet(UserId);
            wallet.AddCreditCard(creditCard);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => wallet.ChangeWalletLimit(newLimit));
        }

        [Fact]
        public void Should_WalletAvailableLimit_EqualsTo_AvailableSumCreditCardLimit()
        {
            // Arrange
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var newLimit = 1000;

            var wallet = new Wallet(UserId);
            wallet.AddCreditCard(creditCard);

            // Act
            wallet.ChangeWalletLimit(newLimit);

            // Assert
            Assert.Equal(newLimit, wallet.AvailableCreditCardsLimit);
        }

        [Fact]
        public void Should_CannotMakeAPurchase_When_WalletNotContainsAnyCreditCard()
        {
            // Arrange
            var wallet = new Wallet(UserId);

            // Act
            Assert.Throws<InvalidOperationException>(() => wallet.Buy(500));
        }

        [Fact]
        public void Should_MakeAPurchase_With_HigherDueDateCreditCard()
        {
            // Arrange
            var wallet = new Wallet(UserId);
            var higherDueDateCreditCard = new CreditCard(Guid.NewGuid(), "Katia", 987654321, 789, 500, DateTime.Now.AddDays(60), DateTime.Now.AddYears(1));
            var anotherCreditCard = new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(higherDueDateCreditCard);
            wallet.AddCreditCard(anotherCreditCard);
            wallet.ChangeWalletLimit(wallet.CreditCards.Sum(a => a.CreditLimit));

            // Act
            wallet.Buy(300);

            // Assert
            var selectedCreditCard = wallet.CreditCards.First(a => a.Number == higherDueDateCreditCard.Number);
            Assert.Equal(200, selectedCreditCard.AvailableCredit);
            Assert.Equal(1200, wallet.AvailableCreditCardsLimit);
        }

        [Fact]
        public void Should_MakeAPurchase_With_MinimumLimitCreditCard()
        {
            // Arrange
            var wallet = new Wallet(UserId);
            var firstCreditCard = new CreditCard(Guid.NewGuid(), "Katia", 543216789, 432, 500, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var minimumLimitCreditCard = new CreditCard(Guid.NewGuid(), "Wellington", 987654321, 789, 350, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(firstCreditCard);
            wallet.AddCreditCard(minimumLimitCreditCard);
            wallet.ChangeWalletLimit(wallet.CreditCards.Sum(a => a.CreditLimit));

            // Act
            wallet.Buy(175);

            // Assert
            Assert.Equal(175, wallet.CreditCards.First(a => a.Number == minimumLimitCreditCard.Number).AvailableCredit);
            Assert.Equal(675, wallet.AvailableCreditCardsLimit);
        }

        [Fact]
        public void Should_MakeAPurchase_With_MultipleCreditCardsAndDifferentDueDates()
        {
            // Arrange
            var wallet = new Wallet(UserId);
            var firstCreditCard = new CreditCard(Guid.NewGuid(), "Wellington", 987654321, 789, 500, DateTime.Now.AddDays(60), DateTime.Now.AddYears(1));
            var secondCreditCard = new CreditCard(Guid.NewGuid(), "Katia", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            var thirdCreditCard = new CreditCard(Guid.NewGuid(), "Leonardo", 123459876, 143, 250, DateTime.Now.AddDays(45), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(firstCreditCard);
            wallet.AddCreditCard(secondCreditCard);
            wallet.AddCreditCard(thirdCreditCard);
            wallet.ChangeWalletLimit(wallet.CreditCards.Sum(a=> a.CreditLimit));

            // Act
            wallet.Buy(1300);

            // Assert
            Assert.Equal(0, wallet.CreditCards.First(card => card.Number == firstCreditCard.Number).AvailableCredit);
            Assert.Equal(0, wallet.CreditCards.First(card => card.Number == thirdCreditCard.Number).AvailableCredit);

            Assert.Equal(450, wallet.CreditCards.First(card => card.Number == secondCreditCard.Number).AvailableCredit);
            Assert.Equal(450, wallet.AvailableCreditCardsLimit);
        }

        [Fact]
        public void Should_MakeAPurchase_With_MultipleCreditCardsAndSimilarDueDates()
        {
            // Arrange
            var wallet = new Wallet(UserId);
            var firstCreditCard = new CreditCard(Guid.NewGuid(), "Wellington", 987654321, 789, 500, DateTime.Now.AddDays(60), DateTime.Now.AddYears(1));
            var secondCreditCard = new CreditCard(Guid.NewGuid(), "Katia", 123456789, 123, 1000, DateTime.Now.AddDays(60), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(firstCreditCard);
            wallet.AddCreditCard(secondCreditCard);
            wallet.ChangeWalletLimit(wallet.CreditCards.Sum(a => a.CreditLimit));

            // Act
            wallet.Buy(1450);

            // Assert
            Assert.Equal(0, wallet.CreditCards.First(card => card.Number == firstCreditCard.Number).AvailableCredit);
            Assert.Equal(50, wallet.CreditCards.First(card => card.Number == secondCreditCard.Number).AvailableCredit);
            Assert.Equal(50, wallet.AvailableCreditCardsLimit);
        }

        [Fact]
        public void Cannot_MakeAPurchase_When_InsuficientAvailableCreditLimit()
        {
            // Arrange
            var wallet = new Wallet(UserId);
            var creditCard = new CreditCard(Guid.NewGuid(), "Wellington", 987654321, 789, 500, DateTime.Now.AddDays(60), DateTime.Now.AddYears(1));

            wallet.AddCreditCard(creditCard);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => wallet.Buy(673));
        }
    }
}
