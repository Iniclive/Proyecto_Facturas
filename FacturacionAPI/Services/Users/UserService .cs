using FacturacionAPI.Application.Auth.Dtos;
using FacturacionAPI.Services.Users.Dtos;
using FacturacionAPI.Shared.Abstractions;
using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Users
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        private readonly ICurrentUserService _currentUser;

        public UserService(FacturacionDataService dataService, ICurrentUserService currentUser)
        {
            _repository = dataService.UserRepository;
            _currentUser = currentUser;
        }


        public async Task<Result<List<User>>> GetAllAsync()
        {
            if (!_currentUser.IsAdmin)
                return Failure<List<User>>("UserForbidden", "No tienes permiso para acceder a este recurso.", ErrorType.Forbidden);

            var users = await _repository
                .Query(UserProjections.BaseTable)
                .ToListAsync();

            return Result<List<User>>.Success(users);
        }


        public async Task<Result<User>> CreateAsync(RegisterUserDto userReg)
        {
            if (userReg == null)
                return Failure<User>("UserBadRequest", "Los datos del usuario son nulos.", ErrorType.BadRequest);

            var emailExists = await _repository
                .Query(UserProjections.BaseTable)
                .Where(UserFields.Email, userReg.Email)
                .AnyAsync();

            if (emailExists)
                return Failure<User>("EmailAlreadyRegistered", "El email ya está registrado.", ErrorType.BadRequest);

            var newUser = new User
            {
                Name = userReg.Name,
                Email = userReg.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userReg.Password),
                Role = "user"
            };

            await _repository.SaveAsync(newUser);

            var userWithId = await _repository
                .Query(UserProjections.BaseTable)
                .GetAsync(UserFields.Email, userReg.Email);

            return Result<User>.Success(userWithId);
        }

        public async Task<Result<User>> UpdateAsync(User updatedUser)
        {
            if (updatedUser == null)
                return Failure<User>("UserBadRequest", "Los datos del usuario son nulos.", ErrorType.BadRequest);

            var previousUser = await _repository
                .GetAsync(UserProjections.BaseTable, updatedUser.IdUser);

            if (previousUser == null)
                return Failure<User>("UserNotFound", $"Usuario con id '{updatedUser.IdUser}' no encontrado.", ErrorType.NotFound);

            if (!_currentUser.IsAdmin && previousUser.IdUser != _currentUser.UserId)
                return Failure<User>("UserForbidden", "No tienes permiso para modificar este usuario.", ErrorType.Forbidden);

            if (previousUser.Email != updatedUser.Email)
            {
                var emailExists = await _repository
                    .Query(UserProjections.BaseTable)
                    .Where(UserFields.Email, updatedUser.Email)
                    .AnyAsync();

                if (emailExists)
                    return Failure<User>("EmailAlreadyRegistered", "El email ya está registrado.", ErrorType.BadRequest);
            }

            previousUser.Name = updatedUser.Name;
            previousUser.Email = updatedUser.Email;

            if (!string.IsNullOrEmpty(updatedUser.Password))
                previousUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);

            await _repository.SaveAsync(previousUser);

            previousUser.Password = null; // no exponer el hash al frontend
            return Result<User>.Success(previousUser);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (!_currentUser.IsAdmin)
                return Result.Failure(new Error("UserForbidden", "...", ErrorType.Forbidden));

            if (id <= 0)
                return Result.Failure(new Error("UserBadRequest", "El id del usuario no es válido.", ErrorType.BadRequest));

            _repository.DeleteUserWithInvoices(id);
            return Result.Success();
        }


        private static Result<T> Failure<T>(string code, string message, ErrorType type)
            => Result<T>.Failure(new Error(code, message, type));

        public async Task<Result> UpdatePasswordAsync(UpdatePasswordRequestDto request)
        {
            var user = await _repository.GetAsync(UserProjections.BaseTable, _currentUser.UserId);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
            {
                return Result.Failure(new Error(
                    Code: "InvalidCredentials",
                    Message: "Credenciales inválidas",
                    Type: ErrorType.BadRequest
                ));
            }

            if (string.IsNullOrEmpty(request.NewPassword))
            {
                return Result.Failure(new Error(
                    Code: "BadRequest",
                    Message: "Nueva contraseña invalida",
                    Type: ErrorType.BadRequest
                ));
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _repository.UpdateAsync(user, UserFields.Password);

            return Result.Success();
        }

        public async Task<Result> UpdateEmailAsync(UpdateEmailDto request) {

            var user = await _repository.GetAsync(UserProjections.BaseTable, _currentUser.UserId);

            if (user == null)
            {
                return Result.Failure(new Error(
                    Code: "InvalidUserID",
                    Message: "Usuario no encontrado",
                    Type: ErrorType.NotFound
                ));
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return Result.Failure(new Error(
                    Code: "InvalidEmailInformation",
                    Message: "Nuevo email invalida",
                    Type: ErrorType.BadRequest
                ));
            }

            if (user.Email != request.Email)
            {
                var emailExists = await _repository
                    .Query(UserProjections.BaseTable)
                    .Where(UserFields.Email, request.Email)
                    .AnyAsync();

                if (emailExists)
                    return Result.Failure(new Error(
                    Code: "DuplicatedEmail",
                    Message: "El email ya esta registrado",
                    Type: ErrorType.BadRequest
                ));
            }

            user.Email = request.Email;
            await _repository.UpdateAsync(user, UserFields.Email);

            return Result.Success();
        }
        public async Task<Result> UpdateNameAsync(UpdateNameDto request) {

            var user = await _repository.GetAsync(UserProjections.BaseTable, _currentUser.UserId);

            if (user == null)
            {
                return Result.Failure(new Error(
                    Code: "InvalidUserID",
                    Message: "Usuario no encontrado",
                    Type: ErrorType.NotFound
                ));
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return Result.Failure(new Error(
                    Code: "BadRequest",
                    Message: "Nuevo nombre de usuario invalido",
                    Type: ErrorType.BadRequest
                ));
            }
            user.Name = request.Name;
            await _repository.UpdateAsync(user, UserFields.Name);
            return Result.Success();
        }
    }
}