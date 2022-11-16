namespace YF.Domain.Entities;

public class Currency : BaseEntity
{
    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;
}
