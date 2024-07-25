using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Domain.Models;

namespace Domain.Interfaces
{
    /// <summary>
    /// Defines the methods for a repository that provides CRUD operations and other data access functionalities.
    /// </summary>
    /// <typeparam name="T">The type of the entity managed by the repository.</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="Id">The unique identifier of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        Task<T> GetByIdAsync(int Id);

        /// <summary>
        /// Retrieves all entities with pagination metadata asynchronously.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of entities per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PagedResult{T}"/> object with the entities and pagination metadata.</returns>
        Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <typeparam name="T">The type of entities to retrieve.</typeparam>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains an enumerable of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateAsync(T entity);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Updates an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Retrieves entities based on a specified condition asynchronously.
        /// </summary>
        /// <param name="predicate">An expression that defines the condition to filter the entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities that match the specified condition.</returns>
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Executes a stored procedure asynchronously.
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
        /// <param name="parameters">The parameters to pass to the stored procedure.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities returned by the stored procedure.</returns>
        Task<IEnumerable<T>> ExecuteStoredProcedureAsync(string storedProcedure, params SqlParameter[] parameters);
    }
}

