using inercya.EntityLite;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

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

    }
}
