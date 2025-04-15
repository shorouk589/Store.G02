﻿using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        //private readonly Dictionary<string, object> _repositories;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            // _repositories = new Dictionary<string, object>();

            _repositories = new ConcurrentDictionary<string, object>();
        }
      
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>

       => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_context));






        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}




