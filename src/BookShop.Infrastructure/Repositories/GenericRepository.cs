using System.Linq.Expressions;
using BookShop.Domain.Common;
using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.Repositories
{
    /// <summary>
    /// Generic abstract class to implement generic methods of 
    /// each entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Database connection handle to commuicate with database
        /// </summary>
        protected readonly BookShopDbContext BookShopDb;

        /// <summary>
        ///  declare a Generic table to assign each table/entity
        ///  to perform  database operation.
        /// </summary>
        protected readonly DbSet<TEntity> DbSet;

        protected GenericRepository(BookShopDbContext bookShopDb)
        {
            BookShopDb = bookShopDb;
            DbSet = bookShopDb.Set<TEntity>();
        }

        /// <summary>
        /// get the all the table record/rows from the database
        /// </summary>
        /// <returns></returns>
        public virtual  async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        /// <summary>
        /// get you a single record/row from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual  async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// add a new record into the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChangesAsync();
        }

        /// <summary>
        ///  update the single row in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();

        }

        /// <summary>
        ///  Remove a single row from the database 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }

        /// <summary>
        ///  save the all transaction into the database
        /// </summary>
        /// <returns>return true if changes been saved successfully else return false if issue persist</returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await BookShopDb.SaveChangesAsync();
        }

        /// <summary>
        ///  Generic Search with the specific condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>return the list of entities pass for the specific condition pass to it</returns>
        public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// to avoid multiple instance  of db connection  which normally cost performance issue
        ///  destroy the dabase connection handle
        /// </summary>
        public void Dispose()
        {
            BookShopDb?.Dispose();
        }
    }
}