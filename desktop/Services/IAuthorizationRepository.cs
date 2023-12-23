using System;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop;

public interface IAuthorizationRepository
{
    [Post("/autorization/login")]
    public Task<Token> Login([Body] Authorization authorization);
    [Post("/authorization/updatetoken")]
    public Task<Token> UpdateToken([Header("RefreshToken")] string refreshToken);
    [Post("/authorization/logout")]
    public Task Logout([Authorize("Bearer")] string accessToken);
}
