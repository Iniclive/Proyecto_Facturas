using FacturacionAPI.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Products
{
    public interface IProductService
    {
        Task<Result<Product>> GetProductByIdAsync(int id);
        Task<Result<List<Product>>> GetAllProductsAsync(string? searchTerm = null);
        Task<Result<Product>> SaveNewProductAsync(Product newProduct);
        Task<Result<Product>> UpdateProductAsync(Product updatedProduct);
        Task<Result> DeleteProductAsync(int id);    
    }
}
