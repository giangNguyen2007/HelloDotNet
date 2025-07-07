using Microsoft.AspNetCore.Identity;

namespace Game.API.Model;

public class AppUserModel : IdentityUser
{
    private string? Genre { get; set; }
}
