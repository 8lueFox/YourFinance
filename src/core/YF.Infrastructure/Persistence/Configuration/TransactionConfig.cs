using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YF.Domain.Entities;

namespace YF.Infrastructure.Persistence.Configuration;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasOne(x => x.Currency);
        builder
            .HasOne(x => x.Stock);
    }
}
