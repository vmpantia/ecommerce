using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.Requests;

namespace ECommerce.BAL.Contractors
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task SaveProductAsync(SaveProductRequest request);
    }
}