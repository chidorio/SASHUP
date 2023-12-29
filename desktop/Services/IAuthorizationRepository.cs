using System;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop;

public interface IAuthorizationRepository
{
    [Post("/Autorization/Login")]
    public Task<Token> Login([Body] Authorization authorization);
    [Post("/Autorization/UpdateToken")]
    public Task<Token> UpdateToken([Header("RefreshToken")] string refreshToken);
    [Post("/Autorization/Logout")]
    public Task Logout([Authorize("Bearer")] string accessToken);
}
