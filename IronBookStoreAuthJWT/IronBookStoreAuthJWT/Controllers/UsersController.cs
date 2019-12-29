using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronBookStoreAuthJWT.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronBookStoreAuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SecurityManager, Administrator")] //TODO: roles are case-sensitive
    public class UsersController : ControllerBase
    {
        private readonly IBookStoreRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IBookStoreRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetUsers();
            var model = _mapper.Map<IEnumerable<Core.Dtos.User>>(users);
            return Ok(model);
        }
    }
}