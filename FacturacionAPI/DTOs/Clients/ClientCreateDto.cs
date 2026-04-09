namespace FacturacionAPI.DTOs.Clients
{
    public class ClientCreateDto
    {
        public string LegalName{ get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CommercialName { get; set; } = string.Empty;
        public string Cif { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;


    }
}
