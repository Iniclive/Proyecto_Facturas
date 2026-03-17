using FacturacionAPI.DTOs.Facturas;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.DTOs.Lineas
{
    public class LineaFacturaResponseDto
    {
        public LineaFactura Linea { get; set; }
        public FacturaResumenDto Factura { get; set; }
    }
}
