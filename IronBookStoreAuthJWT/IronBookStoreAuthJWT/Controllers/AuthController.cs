using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronBookStoreAuthJWT.Core.Dtos;
using IronBookStoreAuthJWT.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronBookStoreAuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IBookStoreRepository _repository;

        public AuthController(ISecurityService securityService,
            IBookStoreRepository repository)
        {
            _securityService = securityService;
            _repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login login) 
        {
            if(login == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _securityService.ValidateCredentials(login.Email, login.Password))
            {
                return BadRequest("Email and/or password not valid.");
            }

            var token = new IronToken(await _securityService.GetToken(login.Email));

            return Ok(token);
        }
    }
}