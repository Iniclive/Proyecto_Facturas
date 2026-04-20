using FacturacionAPI.DTOs.Facturas;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.LineasFactura.Dtos
{
    public class LineaFacturaResponseDto
    {
        public LineaFactura Linea { get; set; }
        public FacturaResumenDto Factura { get; set; }
    }
}
