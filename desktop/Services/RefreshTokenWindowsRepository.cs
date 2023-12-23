using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace desktop;

public class RefreshTokenWindowsRepository : IRefreshTokenRepository
{
    private readonly byte[] entropy = {1,5,6,12,5,62,3,56,1,56,3};
    private readonly SqLiteContext _context;
    public RefreshTokenWindowsRepository(SqLiteContext sqLiteContext)
    {
        _context = sqLiteContext;
    }
    private string ProtectToken(string token)
    {
        byte[] tokenBytes = Encoding.Unicode.GetBytes(token);
        byte [] encryptBytes = ProtectedData.Protect(tokenBytes,entropy,DataProtectionScope.CurrentUser);
        return Convert.ToBase64String(encryptBytes); 
    }
    private string UnprotectToken(string protectToken)
    {
        byte[] encryptedBytes = Convert.FromBase64String(protectToken);
        byte[] tokenBytes = ProtectedData.Unprotect(encryptedBytes,entropy,DataProtectionScope.CurrentUser);
        return Encoding.Unicode.GetString(tokenBytes); 
    }
    public async Task AddRefreshToken(string token)
    {
        string protectToken = await Task.Run(()=>ProtectToken(token));
        _context.RefreshTokens.Add(new(){Token=protectToken});
        await _context.SaveChangesAsync();
    }
    public async Task DeleteRefreshToken()
    {
        _context.RefreshTokens.Remove(_context.RefreshTokens.FirstOrDefault());
        await _context.SaveChangesAsync();
    }
    public async Task<string?> GetRefreshToken()
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync();
        if(refreshToken == null)
            return null;
        string token = await Task.Run(()=>UnprotectToken(refreshToken.Token));
        return token;
    }
    public async Task UpdateRefreshToken(string token)
    {
        DeleteRefreshToken();
        AddRefreshToken(token);
    }
}
