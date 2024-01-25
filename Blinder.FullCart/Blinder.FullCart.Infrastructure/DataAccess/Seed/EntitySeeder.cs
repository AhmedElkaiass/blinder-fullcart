using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blinder.FullCart.Infrastructure.DataAccess.Seed;

internal static class EntitySeeder
{
    public static void Seed<TEntity>(this EntityTypeBuilder<TEntity> builder, IEntitySeed<TEntity> entitySeed) where TEntity : class
    {
        builder.HasData(entitySeed.GetEntitiesToSeed());
    }
}
internal interface IEntitySeed<out TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetEntitiesToSeed();
}
