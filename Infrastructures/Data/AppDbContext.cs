using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        //public async Task<List<MainCategory>> GetAllMainCategoriesAsync()
        //{
        //    return await MainCategories.FromSqlRaw("EXEC GetAllMainCategories").ToListAsync();
        //}

        //public async Task<List<SubCategory>> GetSubCategoriesByMainCategoryIdAsync(int mainCategoryId)
        //{
        //    var parameter = new SqlParameter("@MainCategoryId", mainCategoryId);
        //    return await SubCategories.FromSqlRaw("EXEC GetSubCategoriesByMainCategoryId @MainCategoryId", parameter).ToListAsync();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Book>()
                    .Property(b => b.Price)
                    .HasColumnType("decimal(18,2)"); 



            // Seed data 



        }
    }
    }

