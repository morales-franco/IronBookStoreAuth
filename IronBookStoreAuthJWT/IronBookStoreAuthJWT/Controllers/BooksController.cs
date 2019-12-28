using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronBookStoreAuthJWT.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronBookStoreAuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //TODO: Apply Authorization - User must be login
    public class BooksController : ControllerBase
    {
        private readonly IBookStoreRepository _repository;
        private readonly IMapper _mapper;

        public BooksController(IBookStoreRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize(Policy = "UserMustBeGeneralManagerOrUpper")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetBooks();
            var books = _mapper.Map<IEnumerable<Core.Dtos.Book>>(entities);
            return Ok(books);
        }

        [Authorize(Policy = "UserMustBeGeneralManagerOrUpper")]
        [HttpGet("{bookId}", Name = "GetBook")]
        public async Task<IActionResult> Get(Guid bookId)
        {
            var entity = await _repository.GetBook(bookId);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Core.Dtos.Book>(entity));
        }

        [Authorize(Policy = "UserMustBeGeneralManagerOrUpper")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Core.Dtos.BookForCreation book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Core.Entities.Book>(book);

            await _repository.AddBook(entity);

            if (!await _repository.SaveAsync())
            {
                throw new Exception("Adding a book failed on save.");
            }

            var bookCreated = _mapper.Map<Core.Dtos.Book>(entity);

            return CreatedAtRoute("GetBook",
                new { bookId = bookCreated.BookId },
                bookCreated);
        }

        [Authorize(Policy = "UserMustBeGeneralManagerOrUpper")]
        [HttpPut("{bookId}")]
        public async Task<IActionResult> Update(Guid bookId,
            [FromBody]Core.Dtos.BookForUpdate book)
        {
            if (book == null)
            {
                return NotFound();
            }

            if (!await _repository.BookExists(bookId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Core.Dtos.BookForUpdate, Core.Entities.Book>(book, opt =>
            {
                opt.AfterMap((s, d) => d.BookId = bookId);
            });

            await _repository.UpdateBook(entity);

            if (!await _repository.SaveAsync())
            {
                throw new Exception("Updating a book failed on save.");
            }

            return NoContent();
        }


        [Authorize(Policy = "UserMustBeBookOwner")]
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Remove(Guid bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            if (!await _repository.BookExists(bookId))
            {
                return NotFound();
            }

            var entity = await _repository.GetBook(bookId);

            await _repository.DeleteBook(entity);

            if (!await _repository.SaveAsync())
            {
                throw new Exception("Deleting a book failed on save.");
            }

            return NoContent();
        }

    }
}