using FacturacionAPI.Application.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => FromResult(await _clientService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => FromResult(await _clientService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client newClient)
            => FromResult(await _clientService.CreateAsync(newClient));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Client updatedClient)
            => FromResult(await _clientService.UpdateAsync(updatedClient));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => FromResult(await _clientService.DeleteAsync(id));
    }
}