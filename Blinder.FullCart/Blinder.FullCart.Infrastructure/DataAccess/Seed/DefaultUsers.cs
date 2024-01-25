using Blinder.FullCart.Domain.Users;
namespace Blinder.FullCart.Infrastructure.DataAccess.Seed;
public class DefaultUsers : IEntitySeed<AppUser>
{
    public IEnumerable<AppUser> GetEntitiesToSeed() => new List<AppUser> {
            new()
            {
                Id=1,
                ConcurrencyStamp= "a9978a15-e91e-46bb-b388-34123ee97000",
                UserName = "Admin",
                Email="admin@fullCart.com",
                PhoneNumber="567016337",
                SecurityStamp="a9978a15-e91e-46bb-b388-34123ee97000",
                NormalizedEmail="ADMIN@FULLCART.COM",
                PasswordHash="AQAAAAEAACcQAAAAEIqNXupOSGwEf9MnPO/QXZncOF3wqOQK9wFNolSkPzTUhI5cDeqE5zQLyrTk+9mLKQ==", // => P@ssw0rd,
                UserRoles = new List<AppUserRole>()
                {
                    new(){RoleId=1,UserId=1}
                }
            },
    };
}