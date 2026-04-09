using inercya.EntityLite;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;
using FacturacionAPI.DTOs.Users;
using Microsoft.IdentityModel.Tokens;
using inercya.EntityLite.Extensions;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;
        public UsersController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>>GetAllUsers()
        {
            if (!User.IsInRole("admin"))
            {
                return Forbid();
            }
            var users = await _dataService.UserRepository
                .Query(UserProjections.BaseTable)
                .ToListAsync();       
            return Ok(users);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserRegisteredDto>> CreateUser(RegisterUserDto userReg)
        {
            try
            {
                bool email = await _dataService.UserRepository
                    .Query(UserProjections.BaseTable)
                    .Where(UserFields.Email, userReg.Email)
                    .AnyAsync();
                if (email)
                {
                    return BadRequest("El email ya está registrado.");
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userReg.Password);
                var newUser = new User
                {
                    Name = userReg.Name,
                    Email = userReg.Email,
                    Password = passwordHash,
                    Role = "user"
                };

                await _dataService.UserRepository.SaveAsync(newUser);
                var userWithId =  await _dataService.UserRepository
                    .Query(UserProjections.BaseTable)
                    .GetAsync(UserFields.Email, userReg.Email);
                    
                return Ok(userWithId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User updatedUser)
        {
            try
            {
                if (updatedUser == null)
                {
                    return BadRequest("Los datos del usuario son nulos.");
                }
                var previusUser = await _dataService.UserRepository
                .GetAsync(UserProjections.BaseTable, updatedUser.IdUser);
               
                previusUser.Name = updatedUser.Name;
                previusUser.Email = updatedUser.Email;
                if (!string.IsNullOrEmpty(updatedUser.Password)) {
                previusUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
                }

                await _dataService.UserRepository.SaveAsync(previusUser);
                
                return Ok(previusUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el usuario: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!User.IsInRole("admin"))
            {
                return Forbid();
            }
            try
            {
                _dataService.UserRepository.DeleteUserWithInvoices(id);                 
                return NoContent(); // 204 -> eliminación correcta
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la factura: {ex.Message}");
            }
        }

    }
}
