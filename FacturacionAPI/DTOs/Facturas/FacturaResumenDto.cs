namespace FacturacionAPI.DTOs.Facturas
{
    public class FacturaResumenDto
    {
        public decimal? Importe { get; set; }
        public decimal? ImporteIva { get; set; }
        public decimal? ImporteTotal { get; set; }
        public DateTime Modificado { get; set; }
        public int ModificadoPor { get; set; }
    }
}
