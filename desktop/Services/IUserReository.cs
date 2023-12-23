using System;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop;

public interface IUserReository
{
    [Get("/User/GetInfo")]
    public Task<User> GetUserInfo([Authorize("Bearer")] string accessToken);
}
