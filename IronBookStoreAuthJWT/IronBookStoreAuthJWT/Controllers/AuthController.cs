using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronBookStoreAuthJWT.Core.Constants;
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
        private readonly IMapper _mapper;

        public AuthController(ISecurityService securityService,
            IBookStoreRepository repository,
            IMapper mapper)
        {
            _securityService = securityService;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
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

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUser register)
        {
            if (register == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<Core.Entities.User>(register);

            //By default all user must be Administrators and General managers
            if (!await _securityService.Register(user, Roles.Administrator, Roles.GeneralManager))
            {
                return BadRequest("Your account has not been created");
            }

            return Ok();
        }
    }
}