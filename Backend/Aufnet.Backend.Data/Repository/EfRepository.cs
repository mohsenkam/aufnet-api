using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Aufnet.Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Data.Repository
{
    /// <summary>
    /// The EF-dependent, generic repository implementation for CRUD operations
    /// </summary>
    /// <typeparam name = "T" > Type of entity for this Repository.</typeparam>
    public class EfRepository<T> : IRepository<T>, IDisposable where T : Entity
    {
        protected DbSet<T> DbSet { get; private set; }
        //protected IObjectSet<T> ObjectSet { get;private set; }
        protected Microsoft.EntityFrameworkCore.DbContext DbContext { get; set; }
        //private readonly ILog _logger = LogManager.GetLogger("EntityFrameWork");


        public EfRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            this.DbContext = context;

            //var objectContext = (DbContext as IObjectContextAdapter).ObjectContext;
            //var objectSet = objectContext.CreateObjectSet<T>();
            //objectSet.EnablePlanCaching = false;
            //ObjectSet = objectSet;

            DbSet = DbContext.Set<T>();
            DbContext.Set<T>();
        }


        public virtual IList<T> GetAll()
        {
            try
            {
                return Query().ToList();
            }
            catch (Exception)
            {
                //_logger.ErrorFormat("Exception occured when trying to GetAll from DB: " + e.Message);
                return null;
            }
        }

        //public IList<T> GetAll(SortOptions sortOptions)
        //{
        //    var query = GetAll();
        //    return (sortOptions.SortDirection == SortDirection.Descending
        //               ? query.OrderByDescending(sortOptions.SortProperty)
        //               : query.OrderBy(sortOptions.SortProperty)).ToList();
        //}

        public IQueryable<T> Query()
        {
            try
            {
                return DbSet.Where(m => !m.IsArchived);
            }

            catch (Exception e)
            {
                //_logger.ErrorFormat("Exception occured when trying to Query the DB: " + e.Message);
                return null;
            }

        }

        public IPagedList<T> GetAllPaged(int pageIndex, int pageItems, SortOptions sortOptions = null)
        {
            return new PagedList<T>(Query().OrderBy(x => x.CreatedAt).Skip((pageIndex - 1) * pageItems).Take(pageItems), pageIndex, pageItems, DbSet.Count());
        }

        public IPagedList<T> GetPaged(int pageIndex, int pageItems, Expression<Func<T, bool>> predicate, SortOptions sortOptions = null)
        {
            return new PagedList<T>(Query().Where(predicate).OrderBy(x => x.CreatedAt).Skip((pageIndex - 1) * pageItems).Take(pageItems), pageIndex, pageItems, DbSet.Count());
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return Query().Where(predicate);
            }
            catch (Exception e)
            {
                //_logger.ErrorFormat("Exception occured when trying to Query the DB with expression: " + e.Message);
                return null;
            }
        }

        public virtual IQueryable<T> QueryAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate);
            }
            catch (Exception e)
            {
                //_logger.ErrorFormat("Exception occured when trying to QueryAll the DB with expression: " + e.Message);
                return null;
            }
        }

        public virtual T GetById(long id)
        {
            return Query().First(x => x.Id == id);
        }

        public T FindOne(Expression<Func<T, bool>> query)
        {
            return Query(query).Single();
        }

        public IList<T> Find(Expression<Func<T, bool>> query)
        {
            return Query(query).ToList();
        }

        public virtual void Add(T entity)
        {
            //var identityName = Thread.CurrentPrincipal.Identity.Name;
            entity.CreatedAt = DateTime.Now;
            //entity.CreatedBy = identityName;
            entity.LastUpdatedAt = DateTime.Now;
            //entity.LastUpdatedBy = identityName;
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }

            SaveChanges();
        }

        public virtual void Update(T entity)
        {
            //var identityName = Thread.CurrentPrincipal.Identity.Name;
            entity.LastUpdatedAt = DateTime.Now;
            //entity.LastUpdatedBy = identityName;
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            dbEntityEntry.Property(x => x.CreatedAt).IsModified = false;

            SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }

            SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public void AddOrUpdate(T entity)
        {
            if (entity.IsNew())
                Add(entity);
            else
            {
                Update(entity);
            }
        }

        public void Archive(T entity)
        {
            entity.IsArchived = true;
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.Property(x => x.IsArchived).IsModified = true;

            SaveChanges();
        }

        public void Archive(int id)
        {
            var entity = GetById(id);
            Archive(entity);
        }

        public void StartUnitOfWork()
        {
            throw new NotSupportedException();
        }

        public void CommitUnitOfWork()
        {
            throw new NotSupportedException();
        }

        // todo: need to get rid of this method as its the unitofwork that should take care of this thing
        private void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
            }

            catch (DbUpdateException dbex)
            {
                //_logger.ErrorFormat("Exception occured when trying to save the changes " + dbex.Message);

            }

            //commented out this code as this is a fatal error and should be picked up by global exception handler
            //catch (Exception ex)
            //{
            //    _logger.ErrorFormat("Other Exception occured when trying to save changes the DB: " + ex.Message);            
            //}

        }

        public void Dispose()
        {
            //DbContext.Dispose();
        }


    }
}