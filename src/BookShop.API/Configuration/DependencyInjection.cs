using BookShop.Domain.Interfaces;
using BookShop.Domain.Services;
using BookShop.Infrastructure.Data;
using BookShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookShop.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, ConfigurationManager Configuration)
        {

            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<BookShopDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("BookShopConnection")));
            services.AddAutoMapper(typeof(Program));




            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

    }
}