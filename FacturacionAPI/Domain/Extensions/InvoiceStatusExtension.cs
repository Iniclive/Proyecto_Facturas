using System.Security.Claims;
using FacturacionAPI.Domain.Enums;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Domain.Extensions
{
    public static class InvoiceStatusExtension
    {
        public static int statusToId(this InvoiceStatusEn status) {
            return (int)status;        
        }
    }
}
