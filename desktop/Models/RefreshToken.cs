using System;
using System.ComponentModel.DataAnnotations;

namespace desktop;

public class RefreshToken
{
    [Key]
    public string Token{get;set;}
}
