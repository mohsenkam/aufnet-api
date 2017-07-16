#region REFERENCES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        TEntity GetById(long id);
        TEntity FindOne(Expression<Func<TEntity, bool>> query);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> query);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        void AddOrUpdate(TEntity entity);

        void Archive(TEntity entity);
        void Archive(int id);


        //Unit of Work
        void StartUnitOfWork();
        void CommitUnitOfWork();
    }
}