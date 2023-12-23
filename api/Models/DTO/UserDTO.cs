using api.Models;

namespace api.Models.DTO;

public class UserDTO
{
    public int IdUser {get;set;}
    public string? Image {get;set;}
    public string Role {get;set;}
    public UserDTO(Autorization authorization)
    {
        IdUser = authorization.Id;
        Image = authorization.Image;
        Role = authorization.IdRoleNavigation.Name;
    }
}
