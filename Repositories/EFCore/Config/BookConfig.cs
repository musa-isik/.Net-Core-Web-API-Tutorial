using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "C# in Depth", Price = 39.99M },
                new Book { Id = 2, Title = "Pro ASP.NET Core MVC", Price = 49.99M },
                new Book { Id = 3, Title = "Entity Framework Core in Action", Price = 44.99M }
            );
        }
    }
}
