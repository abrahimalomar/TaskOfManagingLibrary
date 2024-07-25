using Domain.Models;

namespace Infrastructures.Data.Seed
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed MainCategories if not already seeded
            if (!context.MainCategories.Any())
            {
                context.MainCategories.AddRange(
                    new MainCategory { Name = "Programming", Description = "Books related to programming languages, algorithms, and software development.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new MainCategory { Name = "Databases", Description = "Books covering database design, SQL, and data management.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new MainCategory { Name = "Web Development", Description = "Books about web technologies, front-end, and back-end development.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new MainCategory { Name = "Design Patterns", Description = "Books about design patterns and software architecture.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                );

                await context.SaveChangesAsync();
            }

            // Seed SubCategories if not already seeded
            if (!context.SubCategories.Any())
            {
                context.SubCategories.AddRange(
                    new SubCategory { Name = "Object-Oriented Programming", MainCategoryId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "Functional Programming", MainCategoryId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "Relational Databases", MainCategoryId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "NoSQL Databases", MainCategoryId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "Front-End Development", MainCategoryId = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "Back-End Development", MainCategoryId = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "Creational Patterns", MainCategoryId = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new SubCategory { Name = "Structural Patterns", MainCategoryId = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                );

                await context.SaveChangesAsync();
            }

            // Seed Authors if not already seeded
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author { Name = "Ibrahim Al-Omar", AuthorInfo = "Expert in programming and software architecture.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Author { Name = "Mohammed Omar Al-Omar", AuthorInfo = "Specialist in databases and web development.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Author { Name = "Michael Nygard", AuthorInfo = "Author of 'Release It!', expert in software reliability.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                );

                await context.SaveChangesAsync();
            }

            // Seed Books if not already seeded
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Clean Code",
                        Isbn = "978-0132350884",
                        
                        PageCount = 464,
                        Language = "English",
                        Price = 29.99m,
                        AuthorId = 1, // Ibrahim Al-Omar
                        MainCategoryId = 1,
                        SubCategoryId = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Book
                    {
                        Title = "Release It!",
                        Isbn = "978-1617290299",
                     
                        PageCount = 405,
                        Language = "English",
                        Price = 34.99m,
                        AuthorId = 3, // Michael Nygard
                        MainCategoryId = 1,
                        SubCategoryId = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Book
                    {
                        Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
                        Isbn = "978-0201633610",
                    
                        PageCount = 395,
                        Language = "English",
                        Price = 49.99m,
                        AuthorId = 1, // Ibrahim Al-Omar
                        MainCategoryId = 4,
                        SubCategoryId = 7,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
