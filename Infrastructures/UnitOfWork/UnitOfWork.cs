
using Domain.Interfaces;
using Domain.Models;
using Infrastructures.Data;
using Infrastructures.Repository;
using Microsoft.Extensions.Logging;

namespace Infrastructures.UnitOfWork
{
    // Implementation of the unit of work pattern for managing repositories and saving changes
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        // Constructor to initialize UnitOfWork with repositories and logger dependencies
        public UnitOfWork(
            AppDbContext context,
             ILogger<UnitOfWork> logger,
            ILogger<BaseRepository<Book>> employeeLogger,
            ILogger<BaseRepository<Author>> guestLogger,
            ILogger<BaseRepository<MainCategory>> hotelLogger,
            ILogger<BaseRepository<SubCategory>> roomLogger
         

            )
        {
            this._context = context;
            this._logger = logger;

            // Initialize repositories with context and specific loggers

            books = new BaseRepository<Book>(_context, employeeLogger);
            authors = new BaseRepository<Author>(_context, guestLogger);
            mainCategories = new BaseRepository<MainCategory>(_context, hotelLogger);
            subCategories = new BaseRepository<SubCategory>(_context, roomLogger);
        


        }
        // Repositories for various entity types
        public IBaseRepository<Book> books { get; private set; }
        public IBaseRepository<Author> authors { get; private set; }
        public IBaseRepository<MainCategory> mainCategories { get; private set; }
        public IBaseRepository<SubCategory> subCategories { get; private set; }
      

        

        // Dispose method to disconnect from the database
        public void Dispose()
        {
            _context.Dispose();
        }
        // Save changes asynchronously
        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes: {Message}", ex.Message);
                throw new Exception("An error occurred while saving changes. Please try again later.", ex);
            }
        }

    }
}