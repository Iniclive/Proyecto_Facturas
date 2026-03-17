using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FacturaSimpleController : ControllerBase
    {

        private readonly FacturacionDataService _dataService;

        public FacturaSimpleController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }


        // GET: api/<FacturaSimpleController>
        [HttpGet]
        public ActionResult<IEnumerable<FacturaSimple>> Get()
        {
            var facturas = _dataService.FacturaSimpleRepository.Query(FacturaSimpleProjections.BaseTable).ToList();
            return Ok(facturas);
        }

    }
}
