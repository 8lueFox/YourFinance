namespace YF.Domain.Entities;

public class Currency : BaseEntity, IAggregateRoot
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public virtual List<Transaction> Transactions { get; private set; } = new ();

    public void Update(string name, string code)
    {
        if(name is not null && !Name.Equals(name)) Name = name;
        if(code is not null && !Code.Equals(code)) Code = code;
    }
}
