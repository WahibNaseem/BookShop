using BookShop.Domain.Entities;

namespace BookShop.Domain.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        /// <summary>
        /// it  gets you all the categories available in the system
        /// </summary>
        /// <returns>it will return categories from the sytem</returns>
        Task<IEnumerable<Category>> GetAllAsync();

        /// <summary>
        /// it helps you to get a single Category through the id of that Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Category> GetByIdAsync(int id);

        /// <summary>
        /// it helps you add the new Category into the system if it doesn't exist
        /// </summary>
        /// <param name="category"></param>
        /// <returns>it will return a Category which has been added in the system</returns>
        Task<Category> AddAsync(Category category);

        /// <summary>
        /// it hleps you to update the categoy into the system if that exist
        /// </summary>
        /// <param name="category"></param>
        /// <returns>it will return the category which has been updated successfully</returns>
        Task<Category> UpdateAsync(Category category);

        /// <summary>
        /// it helps you to remove the category from the system if that exist
        /// </summary>
        /// <param name="category"></param>
        /// <returns>if the category been removed successfully it will true otherwise it will return false</returns>
        Task<bool> RemoveAsync(Category category);

        /// <summary>
        /// It helps you to get the categories from the system if that category exist
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>It will return the categories with respect to the category name you give</returns>
        Task<IEnumerable<Category>> SearchAsync(string categoryName);
    }
}