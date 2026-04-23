using FacturacionAPI.Application.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;


namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService  )
        {
            _productService = productService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? q = null)
        {
            var result = await _productService.GetAllProductsAsync(q);
            return FromResult(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return FromResult(result);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveNewProduct([FromBody] Product newProduct)
        {
            var result = await _productService.SaveNewProductAsync(newProduct);
            return FromResult(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product updatedProduct)
        {
            var result = await _productService.UpdateProductAsync(updatedProduct);
            return FromResult(result);

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return FromResult(result);
        }

    }
}
