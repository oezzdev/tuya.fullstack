using Cards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Contexts;

public class CardsContext(DbContextOptions<CardsContext> options) : DbContext(options)
{
    public virtual DbSet<Card> Cards { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Guid>().HaveConversion<string>();
    }
}
