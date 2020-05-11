﻿using LocalChow.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LocalChow.Persistence.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly LocalChowDbContext _context;
        public RepositoryBase(LocalChowDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
         }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
