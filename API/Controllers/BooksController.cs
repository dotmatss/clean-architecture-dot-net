using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _bookRepository.GetAllBooksAsync());

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BookRequest request)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Price = request.Price
            };

            var created = await _bookRepository.AddBookAsync(book);
            return Ok(created);
        }
    }
}
