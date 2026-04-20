using FacturacionAPI.Domain.Extensions;
using inercya.EntityLite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;
        public ClientsController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsByUser()
        {
            try
            {
                int userId = User.GetUserId();

                var query = _dataService.ClientRepository.Query(ClientProjections.BaseTable);

                if (!User.IsInRole("admin"))
                {
                    query.Where(ClientFields.ClientId, OperatorLite.In, //El uso de OperatorLite permite encadenar subquerys
                        _dataService.UserClientsRepository.Query(UserClientsProjections.BaseTable)
                        .Where(UserClientsFields.UserId, userId)
                        .Fields(FieldsOption.None, "ClientId"));//Esto fuerza a solo recuperar el ClientId
                }

                var clients = await query.ToListAsync();
                return Ok(clients);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Client>> CreateNewClient(Client newClient)
        {
            int userId = User.GetUserId();
            try
            {

                var cifIsRegistered = await _dataService.ClientRepository
                    .Query(ClientProjections.BaseTable)
                    .Where(ClientFields.Cif, newClient.Cif)
                    .FirstOrDefaultAsync();
                if (cifIsRegistered != null) {
                    return BadRequest("El CIF ya está registrado.");
                }
                
                if (newClient.Cif == null || !ValidationExtension.ValidateCIF(newClient.Cif))
                {
                    return BadRequest("El CIF no cumple el formato correcto.");
                }
                _dataService.BeginTransaction();
                try
                {
                    var currentDate = DateTime.Now;
                    newClient.CreationDate = currentDate;
                    await _dataService.ClientRepository.SaveAsync(newClient);
                    var clientWithId = await _dataService.ClientRepository
                        .Query(ClientProjections.BaseTable)
                        .GetAsync(ClientFields.Cif, newClient.Cif);

                    await _dataService.UserClientsRepository
                        .InsertAsync(new UserClients
                        {
                            ClientId = clientWithId.ClientId,
                            UserId = userId,
                            AssignmentDate = currentDate,
                        });
                    _dataService.Commit();
                    return Ok(clientWithId);
                }
                catch (Exception ex)
                {
                    if (_dataService.IsActiveTransaction)
                    {
                        _dataService.Rollback();  
                    }
                    return StatusCode(500, $"Error al crear el cliente : {ex.Message}");
                }                         
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el cliente: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Client>> UpdateClient([FromBody] Client updatedClient)
        {
            int userId = User.GetUserId();
            try
            {
                if (updatedClient == null)
                {
                    return BadRequest("Los datos del usuario son nulos.");
                }
                bool clientUserIdExists = await _dataService.UserClientsRepository
                    .Query(UserClientsProjections.BaseTable)
                    .Where(UserClientsFields.ClientId, updatedClient.ClientId)
                    .And(UserClientsFields.UserId, userId)
                    .AnyAsync();
    
                if (!clientUserIdExists && !User.IsInRole("admin")) {
                    return StatusCode(403, $"El usuario no tiene acceso a este cliente");
                }

                var previusClient = await _dataService.ClientRepository
                .GetAsync(ClientProjections.BaseTable, updatedClient.ClientId);

                previusClient.CommercialName = updatedClient.CommercialName;
                previusClient.LegalName = updatedClient.LegalName;
                previusClient.Email = updatedClient.Email;
                previusClient.Address = updatedClient.Address;
                previusClient.Phone = updatedClient.Phone;

                await _dataService.ClientRepository.SaveAsync(previusClient);

                return Ok(previusClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el cliente: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            int userId = User.GetUserId();

            bool clientUserIdExists = await _dataService.UserClientsRepository
                    .Query(UserClientsProjections.BaseTable)
                    .Where(UserClientsFields.ClientId, id)
                    .And(UserClientsFields.UserId, userId)
                    .AnyAsync();

            if (!clientUserIdExists && !User.IsInRole("admin"))
            {
                return StatusCode(403, $"El usuario no tiene acceso a este cliente");
            }
            try
            {
                _dataService.ClientRepository.DeleteClientAndUserClient(id);
                return NoContent(); // 204 -> eliminación correcta
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el cliente: {ex.Message}");
            }
        }

      
    }
}
