using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using BookShop.Domain.Services;
using Moq;
using Xunit;

namespace BookShop.Domain.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        private readonly Mock<IBookService> _bookService;

        private readonly CategoryService _categoryService;
        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _bookService = new Mock<IBookService>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _bookService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOfCategories_WhenCategoriesExist()
        {
            var categories = CreateCategories();

            _categoryRepositoryMock.Setup(x =>
                x.GetAllAsync()).ReturnsAsync(categories);

            var result = await _categoryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenCategoriesDoNotExist()
        {
            _categoryRepositoryMock.Setup(x =>
                x.GetAllAsync()).ReturnsAsync((List<Category>) null);

            var result = await _categoryService.GetAllAsync();

            Assert.Null(result);
        }

        [Fact]
        public async void GetById_ShouldReturnCategory_WhenCategoryExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                c.GetByIdAsync(category.Id)).ReturnsAsync(category);

            var result = await _categoryService.GetByIdAsync(category.Id);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            _categoryRepositoryMock.Setup(x =>
                x.GetByIdAsync(1)).ReturnsAsync((Category) null);

            var result = await _categoryService.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async void Add_ShouldAddCategory_WhenCategoryNameDoesNotExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(x =>
                x.SearchAsync(c => c.Name == category.Name))
                .ReturnsAsync(new List<Category>());
            _categoryRepositoryMock.Setup(x => x.AddAsync(category));

            var result = await _categoryService.AddAsync(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void Add_ShouldNotAddCategory_WhenCategoryNameAlreadyExist()
        {
            var category = CreateCategory();
            var categories = new List<Category>() { category };

            _categoryRepositoryMock.Setup(x =>
                x.SearchAsync(c => c.Name == category.Name)).ReturnsAsync(categories);

            var result = await _categoryService.AddAsync(category);

            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldUpdateCategory_WhenCategoryNameDoesNotExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(x =>
                x.SearchAsync(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(new List<Category>());
            _categoryRepositoryMock.Setup(c => c.UpdateAsync(category));

            var result = await _categoryService.UpdateAsync(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void Update_ShouldNotUpdateCategory_WhenCategoryDoesNotExist()
        {
            var category = CreateCategory();
            var categories = new List<Category>() { category };

            _categoryRepositoryMock.Setup(x =>
                    x.SearchAsync(x => x.Name == category.Name && x.Id != category.Id))
                .ReturnsAsync(categories);

            var result = await _categoryService.UpdateAsync(category);

            Assert.Null(result);
        }

        [Fact]
        public async void Remove_ShouldRemoveCategory_WhenCategoryDoNotHaveRelatedBooks()
        {
            var category = CreateCategory();

            _bookService.Setup(x =>
                x.GetBooksByCategoryAsync(category.Id)).ReturnsAsync(new List<Book>());

            var result = await _categoryService.RemoveAsync(category);

            Assert.True(result);
        }

        [Fact]
        public async void Search_ShouldReturnAListOfCategory_WhenCategoriesWithSearchedNameExist()
        {
            var categories = CreateCategories();
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(x =>
                x.SearchAsync(x => x.Name.Contains(categoryName)))
                .ReturnsAsync(categories);

            var result = await _categoryService.SearchAsync(searchedCategory.Name);

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        public async void Search_ShouldReturnNull_WhenCategoriesWithSearchedNameDoNotExist()
        {
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(x =>
                x.SearchAsync(x => x.Name.Contains(categoryName)))
                .ReturnsAsync((IEnumerable<Category>) (null));

            var result = await _categoryService.SearchAsync(searchedCategory.Name);

            Assert.Null(result);
        }

        private Category CreateCategory()
        {
            return new Category()
            {
                Id = 1,
                Name = "Category Name 1"
            };
        }

        private List<Category> CreateCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Category Name 1"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Category Name 2"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Category Name 3"
                }
            };
        }
    }
}
