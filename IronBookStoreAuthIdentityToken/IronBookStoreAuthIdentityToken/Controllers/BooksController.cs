using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronBookStoreAuthIdentityToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public class BooksController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult("Hola!"));
        }
    }
}