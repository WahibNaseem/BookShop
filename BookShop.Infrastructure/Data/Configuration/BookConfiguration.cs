using System.Security.Cryptography.X509Certificates;
using BookShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Author)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.PublishDate)
                 .IsRequired();

            builder.Property(x => x.CategoryId)
                  .IsRequired();

            builder.ToTable("Books");

        }
    }
}