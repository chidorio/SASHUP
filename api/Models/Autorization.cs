using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Autorization
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExp { get; set; }

    public int IdRole { get; set; }

    public string? Image { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
