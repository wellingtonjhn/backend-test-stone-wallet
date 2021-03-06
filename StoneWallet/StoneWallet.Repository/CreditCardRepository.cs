﻿using Dapper;
using Microsoft.Extensions.Configuration;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    /// <inheritdoc cref="Repository" />
    /// <summary>
    /// Repositório de Cartão de Crédito
    /// </summary>
    public class CreditCardRepository : Repository, ICreditCardRepository
    {
        /// <inheritdoc />
        /// <summary>
        /// Cria um repositório de Cartão de Crédito
        /// </summary>
        /// <param name="configuration">Configuração da aplicação</param>
        public CreditCardRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc />
        public async Task<CreditCard> Get(Guid id)
        {
            CreditCard card;
            using (var connection = GetConnection())
            {
                const string sql = @"SELECT ID, WALLETID, NUMBER, DUEDATE, EXPIRATIONDATE, PRINTEDNAME, CVV, CREDITLIMIT, AVAILABLECREDIT, CREATIONDATE 
                                     FROM CREDITCARDS WHERE ID = @ID";

                card = await connection.QueryFirstOrDefaultAsync<CreditCard>(sql, new
                {
                    id
                });
            }
            return card;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CreditCard>> GetByWalletId(Guid walletId)
        {
            IEnumerable<CreditCard> cards;
            using (var connection = GetConnection())
            {
                const string sql = @"SELECT ID, WALLETID, NUMBER, DUEDATE, EXPIRATIONDATE, PRINTEDNAME, CVV, CREDITLIMIT, AVAILABLECREDIT, CREATIONDATE 
                                     FROM CREDITCARDS WHERE WALLETID = @WALLETID";

                cards = await connection.QueryAsync<CreditCard>(sql, new { walletId });
            }
            return cards;
        }

        /// <inheritdoc />
        public async Task CreateCreditCard(CreditCard card)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"INSERT INTO CREDITCARDS (ID, WALLETID, NUMBER, DUEDATE, EXPIRATIONDATE, PRINTEDNAME, CVV, CREDITLIMIT, AVAILABLECREDIT, CREATIONDATE)
                                     VALUES (@ID, @WALLETID, @NUMBER, @DUEDATE, @EXPIRATIONDATE, @PRINTEDNAME, @CVV, @CREDITLIMIT, @AVAILABLECREDIT, @CREATIONDATE)";

                await connection.ExecuteAsync(sql, new
                {
                    card.Id,
                    card.WalletId,
                    card.Number,
                    card.DueDate,
                    card.ExpirationDate,
                    card.PrintedName,
                    card.Cvv,
                    card.CreditLimit,
                    card.AvailableCredit,
                    card.CreationDate
                });
            }
        }

        /// <inheritdoc />
        public async Task RemoveCreditCard(CreditCard card)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"DELETE FROM CREDITCARDS WHERE ID = @ID";

                await connection.ExecuteAsync(sql, new
                {
                    card.Id
                });
            }
        }

        /// <inheritdoc />
        public async Task ChangeCardLimits(CreditCard creditCard)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"UPDATE CREDITCARDS 
                                     SET CREDITLIMIT = @CREDITLIMIT, AVAILABLECREDIT = @AVAILABLECREDIT 
                                     WHERE ID = @ID";

                await connection.ExecuteAsync(sql, new
                {
                    creditCard.Id, 
                    creditCard.CreditLimit,
                    creditCard.AvailableCredit
                });
            }
        }
    }
}