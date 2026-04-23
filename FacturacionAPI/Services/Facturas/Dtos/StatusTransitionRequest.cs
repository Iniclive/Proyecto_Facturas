namespace FacturacionAPI.Application.Facturas.Dtos
{
    public record StatusTransitionRequest(
    int IdFactura,
    int EntityRowVersion
     );
}
