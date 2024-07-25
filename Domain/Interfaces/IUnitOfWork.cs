
using Domain.Models;
namespace Domain.Interfaces
{
    // Interface representing a unit of work pattern for managing repositories
    public interface IUnitOfWork : IDisposable
    {
        // Repository for managing Employee entities
        IBaseRepository<Book> books { get; }

        // Repository for managing Hotel entities
        IBaseRepository<Author> authors { get; }

        // Repository for managing Room entities
        IBaseRepository<MainCategory> mainCategories { get; }

        // Repository for managing Guest entities
        IBaseRepository<SubCategory> subCategories { get; }




        // Asynchronously save changes to the underlying data store
        Task SaveAsync();
    }
}