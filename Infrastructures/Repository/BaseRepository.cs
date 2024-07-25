
using Domain.Interfaces;
using Infrastructures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Domain.Models;
using Microsoft.Data.SqlClient;
namespace Infrastructures.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(AppDbContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data by condition: {Message}", ex.Message);
                throw new Exception("An error occurred while fetching data by condition", ex);
            }
        }




        public async Task<T> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid ID {id} passed to GetByIdAsync", id);
                throw new ArgumentOutOfRangeException(nameof(id), "The ID must be greater than zero");
            }

            try
            {
                var entity = await _context.Set<T>().FindAsync(id);

                if (entity == null)
                {
                    throw new ArgumentException($"No {typeof(T).Name} entity found with Id {id}.");
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting entity by Id {Id}", id);
                throw new Exception("An error occurred while retrieving the entity. Please try again later.");
            }
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                _logger.LogError("The entity to be created cannot be null.");
                throw new InvalidOperationException("The entity to be created cannot be null.");
            }

            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating entity: {Message}", ex.Message);
                throw new Exception("An error occurred while creating entity. Please try again later.", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Entity cannot be null.");
            }

            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting entity: {Message}", ex.InnerException?.Message);
                throw new DbUpdateException("Error occurred while deleting entity. Please try again later or contact support.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting entity: {Message}", ex.Message);
                throw new Exception("An unexpected error occurred while deleting entity.", ex);
            }
        }


        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Entity cannot be null.");
            }

            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating entity: {Message}", ex.InnerException?.Message);
                throw new DbUpdateException("Error occurred while updating entity. Please try again later or contact support.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating entity: {Message}", ex.Message);
                throw new Exception("An unexpected error occurred while updating entity.", ex);
            }
        }


        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync(string storedProcedure, params SqlParameter[] parameters)
        {
            try
            {
                return await _context.Set<T>().FromSqlRaw(storedProcedure, parameters).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing stored procedure: {Message}", ex.Message);
                throw new Exception("An error occurred while executing the stored procedure", ex);
            }
        }

        public async Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalItems = await _context.Set<T>().CountAsync();
                var data = await _context.Set<T>()

               
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedResult<T>
                {
                    Data = data,
                    TotalItems = totalItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data: {Message}", ex.Message);
                throw new Exception("An error occurred while fetching data", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data: {Message}", ex.Message);
                throw new Exception("An error occurred while fetching data", ex);
            }
        }
    }
}



