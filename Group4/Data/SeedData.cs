using Group4.Models;

namespace Group4.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // DB has been seeded
        if (context.Books.Any()) return;

        CreateBooks(context);

        context.SaveChanges();
    }

    private static void CreateBooks(ApplicationDbContext context)
    {
        context.Books.AddRange(
            new Book
            {
                Id = "9b0896fa-3880-4c2e-bfd6-925c87f22878",
                Name = "CQRS for Dummies",
                IsAvailable = true
            }, new Book
            {
                Id = "0550818d-36ad-4a8d-9c3a-a715bf15de76",
                Name = "Visual Studio Tips",
                IsAvailable = true
            }, new Book
            {
                Id = "8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1",
                Name = "NHibernate Cookbook",
                IsAvailable = true
            }
        );
    }
}