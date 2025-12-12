using servweb1_t2.Application.DTOs.Book;
using servweb1_t2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using servweb1_t2.Domain.Exceptions;

namespace servweb1_t2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
    {
        var createdBook = await _bookService.CreateBookAsync(createBookDto);
        return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        try
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, updateBookDto);
            return Ok(updatedBook);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }    
}
