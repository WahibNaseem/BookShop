using BookShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                 .IsRequired()
                 .HasColumnType("varchar(100)");


            // 1:N => Cateogry: Books
            builder.HasMany(x => x.Books)
                 .WithOne(x => x.Category)
                 .HasForeignKey(x => x.CategoryId);
        }
    }
}