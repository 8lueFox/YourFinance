using System.ComponentModel.DataAnnotations.Schema;

namespace YF.Domain.Common.Contracts;

public abstract class BaseEntity : BaseEntity<Guid>
{
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; } = default!;

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = new();
}
