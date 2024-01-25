
using Blinder.FullCart.Application.Constants;
using Microsoft.AspNetCore.Identity;

namespace Blinder.FullCart.Infrastructure.DataAccess.Seed;

internal class DefaultRoles : IEntitySeed<IdentityRole<int>>
{
    public IEnumerable<IdentityRole<int>> GetEntitiesToSeed() => new List<IdentityRole<int>> {
        new()
        {
            Id= (int)AppRolesEnm.Admin,
            ConcurrencyStamp= "a9978a15-e91e-46bb-b388-34123ee97074",
            Name = nameof(AppRolesEnm.Admin),
            NormalizedName= nameof(AppRolesEnm.Admin).ToUpper(),
        },
        new()
        {
            Id= (int)AppRolesEnm.Customer,
            ConcurrencyStamp= "a9978a15-e91e-46bb-b388-34123ee97075",
            Name = nameof(AppRolesEnm.Customer),
            NormalizedName= nameof(AppRolesEnm.Customer).ToUpper(),
        }

    };
}
