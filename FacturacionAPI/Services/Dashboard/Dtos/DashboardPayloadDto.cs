using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.DashboardSum.Dtos

{
    public class DashboardPayloadDto
    {
        public int TotalClients { get; set; }
        public decimal TotalFacturado { get; set; }
        public int? TotalUsers { get; set; }
        public Factura[] FacturasRecientes { get; set; }
    }
}
