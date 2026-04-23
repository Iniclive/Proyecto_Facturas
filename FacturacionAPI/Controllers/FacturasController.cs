using FacturacionAPI.Application.Facturas;
using FacturacionAPI.Application.Facturas.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FacturasController : BaseController
{
    private readonly IFacturaService _facturaService;

    public FacturasController(IFacturaService facturaService)
    {
        _facturaService = facturaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => FromResult(await _facturaService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => FromResult(await _facturaService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Factura newInvoice)
        => FromResult(await _facturaService.CreateAsync(newInvoice));

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Factura updatedInvoice)
        => FromResult(await _facturaService.UpdateAsync(updatedInvoice));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => FromResult(await _facturaService.DeleteAsync(id));

    // ─── Transiciones de estado ──────────────────────────────────────

    [HttpPut("{id}/send-to-validate")]
    public async Task<IActionResult> SendToValidate(
    int id,
    [FromBody] RowVersionDto body)
    {
        var request = new StatusTransitionRequest(id, body.EntityRowVersion);
        return FromResult(await _facturaService.SendToValidateAsync(request));
    }

    [HttpPut("{id}/cancel-validation")]
    public async Task<IActionResult> CancelValidation(
        int id,
        [FromBody] RowVersionDto body)
    {
        var request = new StatusTransitionRequest(id, body.EntityRowVersion);
        return FromResult(await _facturaService.CancelValidationAsync(request));
    }

    [HttpPut("{id}/approve")]
    public async Task<IActionResult> Approve(
        int id,
        [FromBody] RowVersionDto body)
    {
        var request = new StatusTransitionRequest(id, body.EntityRowVersion);
        return FromResult(await _facturaService.ApproveAsync(request));
    }
}