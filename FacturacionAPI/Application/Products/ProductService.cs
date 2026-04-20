using FacturacionAPI.Shared.Abstractions;
using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Products
{
    public class ProductService : IProductService
    {

        private readonly ProductRepository _repository;
        private readonly ICurrentUserService _currentUser;

        public ProductService(FacturacionDataService dataservice, ICurrentUserService currentUser)
        {
            _repository = dataservice.ProductRepository;
            _currentUser = currentUser;
        }

        public async Task<Result<Product>> GetProductByIdAsync(int id)
        {
            var result = await GetProductOrFailureAsync(id);
            if (!result.IsSuccess) return result;

            var ownership = CheckOwnership(result.Value!);
            if (!ownership.IsSuccess) return ownership;

            return result;
        }

        public async Task<Result<List<Product>>> GetAllProductsAsync(string? searchTerm)
        {
            var query = _repository.Query(ProductProjections.BaseTable)
                .Where(ProductFields.Active, true);

            if (!_currentUser.IsAdmin)
                query.And(ProductFields.UserId, _currentUser.UserId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query.And(ProductFields.Name, OperatorLite.Contains, searchTerm);

            var products = await query
                .OrderBy(ProductFields.Name)
                .ToListAsync();

            return Result<List<Product>>.Success(products);
        }

        public async Task<Result<Product>> SaveNewProductAsync([FromBody] Product newProduct)
        {

            if (newProduct == null)
            {
                var e = new Error(
               Code: "ProductBadRequest",
               Message: $"Error creating new product",
               Type: ErrorType.BadRequest
                         );

                return Result<Product>.Failure(e);
            }

            newProduct.UserId = _currentUser.UserId;
            newProduct.Active = true;

            await _repository.SaveAsync(newProduct);

            return Result<Product>.Success(newProduct);

        }

        public async Task<Result<Product>> UpdateProductAsync([FromBody] Product updatedProduct)
        {

            if (updatedProduct == null)
            {
                var e = new Error(
                Code: "ProductBadRequest",
                Message: $"Error updating product",
                Type: ErrorType.BadRequest
                    );

                return Result<Product>.Failure(e);
            }
            var result = await GetProductOrFailureAsync(updatedProduct.ProductId);
            if (!result.IsSuccess) return result;

            var ownership = CheckOwnership(result.Value!);
            if (!ownership.IsSuccess) return ownership;

            List<string> fieldsToUpdate = new();
            fieldsToUpdate.Add(ProductFields.Name);
            fieldsToUpdate.Add(ProductFields.DefaultPrice);
            fieldsToUpdate.Add(ProductFields.Description);

            await _repository.UpdateAsync(updatedProduct, fieldsToUpdate.ToArray());

            return Result<Product>.Success(updatedProduct);


        }

        public async Task<Result> DeleteProductAsync(int id)
        {
            var result = await GetProductOrFailureAsync(id);
            if (!result.IsSuccess) return Result.Failure(result.Error!);

            var ownership = CheckOwnership(result.Value!);
            if (!ownership.IsSuccess) return ownership;

            result.Value!.Active = false;
            await _repository.UpdateAsync(result.Value, ProductFields.Active);
            return Result.Success();

        }

        private async Task<Result<Product>> GetProductOrFailureAsync(int id)
        {
            var product = await _repository.GetAsync(ProductProjections.BaseTable, id);
            if (product == null)
            {
                var e = new Error(
                       Code: "ProductNotFound",
                       Message: $"Product with id  '{id}' not found",
                       Type: ErrorType.NotFound
                   );
                return Result<Product>.Failure(e);
            }

            return Result<Product>.Success(product);
        }

        private Result CheckOwnership(Product product)
        {
            if (!_currentUser.IsAdmin && product.UserId != _currentUser.UserId)
                return Result.Failure(new Error("Forbidden", "No tienes acceso a este producto", ErrorType.Forbidden));

            return Result.Success();
        }
    }
}