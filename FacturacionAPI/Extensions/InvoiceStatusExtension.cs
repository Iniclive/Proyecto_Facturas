using System.Security.Claims;
using FacturacionAPI.Enums;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Extensions
{
    public static class InvoiceStatusExtension
    {
        public static int statusToId(this InvoiceStatusEn status) {
            return (int)status;        
        }
    }
}
