using System;
using Aufnet.Backend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.UnitTests.Shared
{
    internal static class TestDbContext
    {
        internal static ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            return context;
        }
    }
    //internal class TestDbAsyncQueryProvider<TEntity> : EntityQueryProvider
    //{
    //    private readonly IQueryCompiler _queryCompiler;
    //    public TestDbAsyncQueryProvider(IQueryCompiler queryCompiler) : base(queryCompiler)
    //    {
    //        _queryCompiler = queryCompiler;
    //    }
    //    public override IQueryable CreateQuery( Expression expression )
    //    {
    //        return new TestDbAsyncEnumerable<TEntity>(expression);
    //    }

    //    public override IQueryable<TElement> CreateQuery<TElement>( Expression expression )
    //    {
    //        return new TestDbAsyncEnumerable<TElement>(expression);
    //    }

    //    public override object Execute( Expression expression )
    //    {
    //        return _queryCompiler.Execute<object>(expression);
    //    }

    //    public override TResult Execute<TResult>( Expression expression )
    //    {
    //        return _queryCompiler.Execute<TResult>(expression);
    //    }

    //    public Task<object> ExecuteAsync( Expression expression, CancellationToken cancellationToken )
    //    {
    //        return Task.FromResult<object>(Execute(expression));
    //    }

    //    public override Task<TResult> ExecuteAsync<TResult>( Expression expression, CancellationToken cancellationToken )
    //    {
    //        return Task.FromResult<TResult>(Execute<TResult>(expression));
    //    }
    //}

    //internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IQueryable<T>
    //{


    //    IQueryProvider IQueryable.Provider
    //    {
    //        get { return new TestDbAsyncQueryProvider<T>(this); }
    //    }

    //    public TestDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
    //    {
    //    }

    //    public TestDbAsyncEnumerable(Expression expression) : base(expression)
    //    {
    //    }
    //}
    //internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    //{
    //    private readonly IEnumerator<T> _inner;

    //    public TestDbAsyncEnumerator( IEnumerator<T> inner )
    //    {
    //        _inner = inner;
    //    }

    //    public void Dispose()
    //    {
    //        _inner.Dispose();
    //    }

    //    public Task<bool> MoveNextAsync( CancellationToken cancellationToken )
    //    {
    //        return Task.FromResult(_inner.MoveNext());
    //    }

    //    public T Current
    //    {
    //        get { return _inner.Current; }
    //    }

    //    object IDbAsyncEnumerator.Current
    //    {
    //        get { return Current; }
    //    }
    //}
}