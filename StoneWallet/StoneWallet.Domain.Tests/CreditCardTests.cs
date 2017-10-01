using StoneWallet.Domain.Models.Entities;
using System;
using Xunit;

namespace StoneWallet.Domain.Tests
{
    public class CreditCardTests
    {
        [Fact]
        public void Shoud_CreateCreditCard()
        {
            var card = new CreditCard("Wellington", 123456789, 123, 1000, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            Assert.NotNull(card);
        }

        [Fact]
        public void Should_DecreaseAvailableLimitFromCard_When_Buy()
        {
            // Arrange
            var credtCardLimit = 1000;
            var card = new CreditCard("Wellington", 123456789, 123, credtCardLimit, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));

            // Act
            card.Buy(300);

            // Assert
            Assert.Equal(700, card.AvailableCredit);
        }

        [Fact]
        public void Should_ReleaseAvailableLimitFromCard_When_PayInvoice()
        {
            // Arrange
            var credtCardLimit = 1000;
            var amount = 300;
            var card = new CreditCard("Wellington", 123456789, 123, credtCardLimit, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
            card.Buy(amount);

            // Act
            card.ReleaseCredit(amount);

            // Assert
            Assert.Equal(credtCardLimit, card.AvailableCredit);
        }
    }
}
