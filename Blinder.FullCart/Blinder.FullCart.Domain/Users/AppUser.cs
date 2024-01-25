using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Blinder.FullCart.Domain.Users;

public class AppUser : IdentityUser<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}
public class AppUserRole : IdentityUserRole<int>
{
   
    public AppUser User { get; set; }
    public IdentityRole Role { get; set; }
}