namespace Blinder.FullCart.Infrastructure.DataAccess.Configurations;
internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder) => builder.Seed(new DefaultRoles());
}
