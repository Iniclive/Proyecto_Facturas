using FacturacionAPI.Application.Insurances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsuranceController : BaseController
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => FromResult(await _insuranceService.GetAllAsync());

        [HttpGet("{searchString}")]
        public async Task<IActionResult> GetBySearchString(string searchString)
            => FromResult(await _insuranceService.GetBySearchStringAsync(searchString));
    }
}