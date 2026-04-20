
using System.Text.RegularExpressions;

namespace FacturacionAPI.Domain.Extensions
{
    public static class ValidationExtension
    {
        public static bool ValidateCIF(string cif)
        {

            string pattern = @"^[ABCDEFGHJKLMNPQRSUVW]{1}[0-9]{7}[A-Z0-9]{1}$";
            Regex regex = new Regex(pattern);
            bool respuesta = regex.IsMatch(cif.ToUpper());
            return respuesta;
              
        }
    }
}
