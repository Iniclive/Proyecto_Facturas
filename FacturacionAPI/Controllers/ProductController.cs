using System.Security.Claims;
using FacturacionAPI.Extensions;
using inercya.EntityLite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;

        public ProductController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/<MaterialController>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {

            int userId = User.GetUserId();

            var query = _dataService.ProductRepository.Query(ProductProjections.BaseTable);

            if (!User.IsInRole("admin")) //Como esta asignado desde el builder podemos usar IsInRole
            {
                query.Where(ProductFields.UserId, userId);
            }

            var products = await query.And(ProductFields.Active, true).OrderBy(ProductFields.Name)
                .ToListAsync();
            return Ok(products);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> SaveNewProduct([FromBody] Product newProduct)
        {
            try 
            {
                if (newProduct == null)
                {
                    return BadRequest("Los datos del material son nulos.");
                }
                int userId = User.GetUserId();
                newProduct.UserId = userId;
                newProduct.Active = true;   
              await _dataService.ProductRepository.SaveAsync(newProduct);
              return Ok(newProduct);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el material: {ex.Message}");
            }
        }

        [HttpGet("{searchString}")]
        public async Task<IActionResult> GetAutoCompleteProducts(string searchString)
        {
            int userId = User.GetUserId();
            try
            {
                var products = await _dataService.ProductRepository
                    .Query(ProductProjections.BaseTable)
                    .Where(ProductFields.Name, OperatorLite.Contains, searchString)
                    .And(ProductFields.UserId, userId)
                    .And(ProductFields.Active, true)
                    .OrderBy(ProductFields.Name)
                    .ToListAsync();

                return Ok(products);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product updatedProduct)
        {
            int userId = User.GetUserId();
            try
            {
                if (updatedProduct == null)
                {
                    return BadRequest("Los datos del usuario son nulos.");
                }
                if(userId != updatedProduct.UserId && !User.IsInRole("admin"))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a este producto");

                }

                var previusProduct = await _dataService.ProductRepository
                .GetAsync(ProductProjections.BaseTable, updatedProduct.ProductId);

                previusProduct.Name = updatedProduct.Name;
                previusProduct.Description = updatedProduct.Description;
                previusProduct.DefaultPrice = updatedProduct.DefaultPrice;

                await _dataService.ProductRepository.SaveAsync(previusProduct);
                return Ok(previusProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el cliente: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
           int userId = User.GetUserId();
           var currentProduct = await _dataService.ProductRepository.GetAsync(ProductProjections.BaseTable, id);

            if (currentProduct == null) {
                return BadRequest("No se ha encontrado el producto");
            }

            if (userId != currentProduct.UserId && !User.IsInRole("admin"))
            {
                return StatusCode(403, $"El usuario no tiene acceso a este producto");

            }
            try
            {
                currentProduct.Active = false;
                await _dataService.ProductRepository.SaveAsync(currentProduct);             
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el cliente: {ex.Message}");
            }
        }

    }
}
