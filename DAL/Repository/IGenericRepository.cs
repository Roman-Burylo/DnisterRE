﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAll();

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entity);

        void Update(TEntity entity);
    }
}
