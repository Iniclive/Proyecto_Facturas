using FacturacionAPI.Application.Users;
using FacturacionAPI.Application.Auth.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;
using FacturacionAPI.Services.Users.Dtos;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => FromResult(await _userService.GetAllAsync());

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterUserDto userReg)
            => FromResult(await _userService.CreateAsync(userReg));

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User updatedUser)
            => FromResult(await _userService.UpdateAsync(updatedUser));

        [Authorize]
        [HttpPut ("change-password")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordRequestDto updatedUser)
            => FromResult(await _userService.UpdatePasswordAsync(updatedUser));

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => FromResult(await _userService.DeleteAsync(id));

        [Authorize]
        [HttpPatch("email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailDto dto)
        => FromResult(await _userService.UpdateEmailAsync(dto));

        [Authorize]
        [HttpPatch("name")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameDto dto)
            => FromResult(await _userService.UpdateNameAsync(dto));


    }
}