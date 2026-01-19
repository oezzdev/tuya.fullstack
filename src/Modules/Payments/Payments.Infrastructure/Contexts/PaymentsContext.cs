using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Contexts;

public class PaymentsContext(DbContextOptions<PaymentsContext> options) : DbContext(options)
{
    public virtual DbSet<Payment> Payments { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Guid>().HaveConversion<string>();
    }
}
