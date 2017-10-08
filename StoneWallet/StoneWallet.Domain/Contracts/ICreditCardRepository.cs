using System;
using System.Collections.Generic;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> Get(Guid id);
        Task<IEnumerable<CreditCard>> GetByWalletId(Guid id);
        Task CreateCreditCard(CreditCard card);
        Task RemoveCreditCard(CreditCard card);
    }
}