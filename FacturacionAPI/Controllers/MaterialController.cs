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
    public class MaterialController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;

        public MaterialController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/<MaterialController>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterials()
        {

            int userId = User.GetUserId();

            var query = _dataService.MaterialRepository.Query(MaterialProjections.BaseTable);

            if (!User.IsInRole("admin")) //Como esta asignado desde el builder podemos usar IsInRole
            {
                query.Where(MaterialFields.UserId, userId);
            }

            var materials = await query.OrderBy(MaterialFields.Name)
                .ToListAsync();
            return Ok(materials);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Material>> SaveNewMaterial([FromBody]Material newMaterial)
        {
            try 
            {
                if (newMaterial == null)
                {
                    return BadRequest("Los datos del material son nulos.");
                }
                int userId = User.GetUserId();                               
               _dataService.MaterialRepository.Save(newMaterial);
                return Ok(newMaterial);
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

    }
}
