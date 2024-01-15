using AutoMapper;
using BookShop.API.Contracts.V1.Book.Requests;
using BookShop.API.Contracts.V1.Book.Responses;
using BookShop.API.Contracts.V1.Category.Requests;
using BookShop.API.Contracts.V1.Category.Responses;
using BookShop.Domain.Entities;

namespace BookShop.API.Configuration
{
    /// <summary>
    /// Configuration the mapping of the domain entities and  data transfer
    /// request and response
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Book, CreateBookRequest>().ReverseMap();
            CreateMap<Book, UpdateBookRequest>().ReverseMap();
            CreateMap<Book, BookResponse>().ReverseMap();
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}