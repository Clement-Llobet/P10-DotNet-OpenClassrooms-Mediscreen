using System.Collections;
using System.Linq.Expressions;

public abstract class QueryableRepositoryBase<T> : IQueryable<T>
{
    private readonly IQueryable<T> _queryable;

    protected QueryableRepositoryBase(IQueryable<T> queryable)
    {
        _queryable = queryable;
    }

    public Type ElementType => _queryable.ElementType;

    public Expression Expression => _queryable.Expression;

    public IQueryProvider Provider => _queryable.Provider;

    public IEnumerator<T> GetEnumerator()
    {
        return _queryable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _queryable.GetEnumerator();
    }
}
