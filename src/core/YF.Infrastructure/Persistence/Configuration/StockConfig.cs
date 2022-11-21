using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YF.Domain.Entities;

namespace YF.Infrastructure.Persistence.Configuration;

public class StockConfig : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder
            .Property(x => x.Company)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Ticker)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(x => x.Sector)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasOne(x => x.Currency);

        builder
            .HasMany(x => x.Transactions);
    }
}
