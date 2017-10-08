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
            var card = CreateCreditCard(1000);
            Assert.NotNull(card);
        }

        [Fact]
        public void Should_DecreaseAvailableLimitFromCard_When_Buy()
        {
            // Arrange
            var creditCardLimit = 1000;
            var card = CreateCreditCard(creditCardLimit);
            var originalCardAvailableCredit = card.AvailableCredit;

            // Act
            card.Buy(300);

            // Assert
            Assert.NotEqual(originalCardAvailableCredit, card.AvailableCredit);
            Assert.Equal(700, card.AvailableCredit);
        }

        [Fact]
        public void Cannot_Buy_When_NotExistsAvailableCredit()
        {
            // Arrange
            var creditCardLimit = 500;
            var card = CreateCreditCard(creditCardLimit);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => card.Buy(600));
        }


        [Fact]
        public void Should_ReleaseAvailableLimitFromCard_When_PayInvoice()
        {
            // Arrange
            var creditCardLimit = 1000;
            var amount = 300;
            var card = CreateCreditCard(creditCardLimit);
            card.Buy(amount);

            // Act
            card.ReleaseCredit(amount);

            // Assert
            Assert.Equal(creditCardLimit, card.AvailableCredit);
        }

        private static CreditCard CreateCreditCard(int creditCardLimit)
        {
            return new CreditCard(Guid.NewGuid(), "Wellington", 123456789, 123, creditCardLimit, DateTime.Now.AddDays(30), DateTime.Now.AddYears(1));
        }
    }
}
