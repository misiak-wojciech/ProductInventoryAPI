using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<IEnumerable<Category>> GetCategoriesWithProductCountAsync();
        Task SaveChangesAsync();
    }
}
