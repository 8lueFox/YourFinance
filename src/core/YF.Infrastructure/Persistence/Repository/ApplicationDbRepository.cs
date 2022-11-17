using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using YF.Application.Common.Persistance;

namespace YF.Infrastructure.Persistence.Repository;

public class ApplicationDbRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class
{
    public ApplicationDbRepository(DbContext dbContext) 
        : base(dbContext)
    {
    }

    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        => specification.Selector is null
        ? base.ApplySpecification(specification)
        : ApplySpecification(specification, false)
            .ProjectToType<TResult>();
}
