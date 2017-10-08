using Dapper;
using Microsoft.Extensions.Configuration;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    /// <inheritdoc cref="Repository" />
    /// <summary>
    /// Repositório de Wallet
    /// </summary>
    public class WalletRepository : Repository, IWalletRepository
    {
        /// <inheritdoc />
        /// <summary>
        /// Cria um repositório da Wallet
        /// </summary>
        /// <param name="configuration">Configuração da aplicação</param>
        public WalletRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc />
        public async Task CreateWallet(Wallet wallet)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"INSERT INTO WALLETS (ID, USERID, WALLETLIMIT, CREATIONDATE) 
                                     VALUES (@ID, @USERID, @WALLETLIMIT, @CREATIONDATE)";

                await connection.ExecuteAsync(sql, new
                {
                    wallet.Id,
                    wallet.UserId,
                    wallet.WalletLimit,
                    wallet.CreationDate
                });
            }
        }

        /// <inheritdoc />
        public async Task<Wallet> GetWalletByUser(Guid userId)
        {
            Wallet wallet;

            using (var connection = GetConnection())
            {
                const string queryWallet = "SELECT ID, USERID, WALLETLIMIT, CREATIONDATE FROM WALLETS WHERE USERID = @USERID;";

                const string queryCreditCards = @"SELECT
	                                                C.ID
	                                                , C.WALLETID
	                                                , C.NUMBER
	                                                , C.DUEDATE
	                                                , C.EXPIRATIONDATE
	                                                , C.PRINTEDNAME
	                                                , C.CVV
	                                                , C.CREDITLIMIT
	                                                , C.AVAILABLECREDIT
	                                                , C.CREATIONDATE AS CARDCREATIONDATE
                                                FROM WALLETS W
                                                INNER JOIN CREDITCARDS C ON W.ID = C.WALLETID
                                                WHERE W.USERID = @USERID;";

                using (var multi = connection.QueryMultiple(queryWallet + queryCreditCards, new { userId }))
                {
                    wallet = await multi.ReadFirstOrDefaultAsync<Wallet>();

                    if (wallet != null)
                    {
                        var creditCards = await multi.ReadAsync<CreditCard>();
                        foreach (var card in creditCards)
                        {
                            wallet.AddCreditCard(card);
                        }
                    }
                }
            }

            return wallet;
        }

        /// <inheritdoc />
        public async Task ChangeWalletLimit(Wallet wallet)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"UPDATE WALLETS SET WALLETLIMIT = @WalletLimit WHERE ID = @ID";

                await connection.ExecuteAsync(sql, new
                {
                    wallet.Id,
                    wallet.WalletLimit
                });
            }
        }
    }
}