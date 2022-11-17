using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YF.Domain.Entities;

namespace YF.Infrastructure.Persistence.Configuration;

public class CurrencyConfig : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder
            .Property(b => b.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
