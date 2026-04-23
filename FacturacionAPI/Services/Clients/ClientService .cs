using FacturacionAPI.Domain.Extensions;
using FacturacionAPI.Shared.Abstractions;
using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Clients
{
    public class ClientService : IClientService
    {
        private readonly FacturacionDataService _dataService;
        private readonly ICurrentUserService _currentUser;

        public ClientService(
            FacturacionDataService dataService,
            ICurrentUserService currentUser)
        {
            _dataService = dataService;
            _currentUser = currentUser;
        }


        public async Task<Result<List<Client>>> GetAllAsync()
        {
            var query = _dataService.ClientRepository.Query(ClientProjections.BaseTable);

            if (!_currentUser.IsAdmin)
            {
                query.Where(ClientFields.ClientId, OperatorLite.In,
                    _dataService.UserClientsRepository
                        .Query(UserClientsProjections.BaseTable)
                        .Where(UserClientsFields.UserId, _currentUser.UserId)
                        .Fields(FieldsOption.None, "ClientId"));
            }

            var clients = await query.ToListAsync();
            return Result<List<Client>>.Success(clients);
        }

        public async Task<Result<Client>> GetByIdAsync(int id)
        {
            var access = await CheckClientAccessAsync(id);
            if (!access.IsSuccess) return access;

            var client = await _dataService.ClientRepository
                .GetAsync(ClientProjections.BaseTable, id);

            return Result<Client>.Success(client);
        }

   

        public async Task<Result<Client>> CreateAsync(Client newClient)
        {
            if (newClient == null)
                return Failure<Client>("ClientBadRequest", "Los datos del cliente son nulos.", ErrorType.BadRequest);

            var cifExistente = await _dataService.ClientRepository
                .Query(ClientProjections.BaseTable)
                .Where(ClientFields.Cif, newClient.Cif)
                .FirstOrDefaultAsync();

            if (cifExistente != null)
                return Failure<Client>("CifAlreadyRegistered", "El CIF ya está registrado.", ErrorType.BadRequest);

            if (newClient.Cif == null || !ValidationExtension.ValidateCIF(newClient.Cif))
                return Failure<Client>("InvalidCif", "El CIF no cumple el formato correcto.", ErrorType.BadRequest);

            _dataService.BeginTransaction();
            try
            {
                var currentDate = DateTime.Now;
                newClient.CreationDate = currentDate;
                await _dataService.ClientRepository.SaveAsync(newClient);

                var clientWithId = await _dataService.ClientRepository
                    .Query(ClientProjections.BaseTable)
                    .GetAsync(ClientFields.Cif, newClient.Cif);

                await _dataService.UserClientsRepository.InsertAsync(new UserClients
                {
                    ClientId = clientWithId.ClientId,
                    UserId = _currentUser.UserId,
                    AssignmentDate = currentDate,
                });

                _dataService.Commit();
                return Result<Client>.Success(clientWithId);
            }
            catch
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                throw;
            }
        }

        public async Task<Result<Client>> UpdateAsync(Client updatedClient)
        {
            if (updatedClient == null)
                return Failure<Client>("ClientBadRequest", "Los datos del cliente son nulos.", ErrorType.BadRequest);

            var access = await CheckClientAccessAsync(updatedClient.ClientId);
            if (!access.IsSuccess) return access;

            var previousClient = await _dataService.ClientRepository
                .GetAsync(ClientProjections.BaseTable, updatedClient.ClientId);

            if (previousClient == null)
                return Failure<Client>("ClientNotFound", $"Cliente con id '{updatedClient.ClientId}' no encontrado.", ErrorType.NotFound);

            previousClient.CommercialName = updatedClient.CommercialName;
            previousClient.LegalName = updatedClient.LegalName;
            previousClient.Email = updatedClient.Email;
            previousClient.Address = updatedClient.Address;
            previousClient.Phone = updatedClient.Phone;

            await _dataService.ClientRepository.SaveAsync(previousClient);

            return Result<Client>.Success(previousClient);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var access = await CheckClientAccessAsync(id);
            if (!access.IsSuccess) return access;

            _dataService.ClientRepository.DeleteClientAndUserClient(id);
            return Result.Success();
        }



        private async Task<Result> CheckClientAccessAsync(int clientId)
        {
            if (_currentUser.IsAdmin) return Result.Success();

            var hasAccess = await _dataService.UserClientsRepository
                .Query(UserClientsProjections.BaseTable)
                .Where(UserClientsFields.ClientId, clientId)
                .And(UserClientsFields.UserId, _currentUser.UserId)
                .AnyAsync();

            return hasAccess
                ? Result.Success()
                : Result.Failure(new Error("Forbidden", "El usuario no tiene acceso a este cliente.", ErrorType.Forbidden));
        }

        private static Result<T> Failure<T>(string code, string message, ErrorType type)
            => Result<T>.Failure(new Error(code, message, type));
    }
}