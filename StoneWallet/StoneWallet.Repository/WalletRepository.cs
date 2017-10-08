using Dapper;
using Microsoft.Extensions.Configuration;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    public class WalletRepository : Repository, IWalletRepository
    {
        public WalletRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

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

        public async Task<Wallet> GetWalletByUser(Guid userId)
        {
            Wallet wallet;

            using (var connection = GetConnection())
            {
                const string sql = @"SELECT ID, USERID, WALLETLIMIT, CREATIONDATE FROM WALLETS WHERE USERID = @USERID";

                wallet = await connection.QueryFirstOrDefaultAsync<Wallet>(sql, new
                {
                    userId
                });
            }
            return wallet;
        }

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