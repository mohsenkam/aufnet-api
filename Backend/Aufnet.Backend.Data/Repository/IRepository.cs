#region REFERENCES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Aufnet.Backend.Data.Models;

#endregion REFERENCES

namespace Aufnet.Backend.Data.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IList<TEntity> GetAll();

        //for generic sorting of entities based on built in property type
        //IList<TEntity> GetAll(SortOptions sortOptions);

        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> QueryAll(Expression<Func<TEntity, bool>> query);

        IPagedList<TEntity> GetAllPaged(int pageIndex, int pageItems, SortOptions sortOptions = null);
        IPagedList<TEntity> GetPaged(int pageIndex, int pageItems, Expression<Func<TEntity, bool>> predicate,
            SortOptions sortOptions = null);

        Task<TEntity> GetByIdAsync(long id);
        TEntity FindOne(Expression<Func<TEntity, bool>> query);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> query);

        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> AddOrUpdateAsync( TEntity entity );

        Task<int> DeleteAsync( TEntity entity);
        Task<int> DeleteAsync( long id);


        Task<int> ArchiveAsync(TEntity entity);
        Task<int> ArchiveAsync( long id);


        //Unit of Work
        void StartUnitOfWork();
        void CommitUnitOfWork();
    }
}