using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SQLitePCL;

namespace desktop;

public interface IRefreshTokenRepository
{
    public Task<string?> GetRefreshToken();
    public Task AddRefreshToken(string token);
    public Task UpdateRefreshToken(string token);
    public Task DeleteRefreshToken();
}
