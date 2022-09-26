﻿using EvalCore.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext Context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            this.Context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await SaveAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            else
                return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int? id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            await SaveAsync();
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task Update(TEntity entityToUpdate)
        {
            var entity = dbSet.Attach(entityToUpdate);   
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            await SaveAsync();
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            dbSet.AttachRange(entitiesToUpdate);
            Context.Entry(entitiesToUpdate).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

    }
}
