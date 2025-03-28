﻿using HouseRentApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace HouseRentApp.Model
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly HousingContext _context;
        
        public HousingContext Context => _context;
        
        protected readonly DbSet<T> _dbSet;

        public Repository(HousingContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}