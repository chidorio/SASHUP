using Microsoft.Identity.Client;

namespace api.Models.DTO;

public class AuthorizationDTO
{
    public string Login {get;set;} = null!;
    public string Password {get;set;} = null!;
}
