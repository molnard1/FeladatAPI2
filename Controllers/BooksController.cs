using AutoMapper;
using FeladatAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeladatAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    // We don't want to use the base type here, it breaks DI
    public class BooksController(BookContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookResponse>>> Get()
        {
            var books = await context.Books.ToListAsync();
            return Ok(mapper.Map<List<GetBookResponse>>(books));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetBookResponse>> Get(Guid id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return Ok(mapper.Map<GetBookResponse>(book));
        }

        [HttpGet("author/{author:guid}")]
        public async Task<ActionResult<List<GetBookResponse>>> GetByAuthor(Guid author)
        {
            var booksByAuthor = await context.Books.Where(book => book.AuthorId == author).ToListAsync();
            return Ok(mapper.Map<List<GetBookResponse>>(booksByAuthor));
        }

        [HttpGet("withauthors")]
        public async Task<ActionResult<List<GetBookWithAuthorResponse>>> GetBooksWithAuthors()
        {
            var booksWithAuthors = await context.Books.Include(b => b.Author).ThenInclude(c => c.Nationality).ToListAsync();
            return Ok(mapper.Map<List<GetBookWithAuthorResponse>>(booksWithAuthors));
        }


        [HttpPost]
        public async Task<ActionResult<GetBookResponse>> Post([FromBody] CreateBookRequest bookDto)
        {
            var book = mapper.Map<Book>(bookDto);

            if (await context.Authors.FindAsync(book.AuthorId) == null) return NotFound();

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = book.BookId }, mapper.Map<GetBookResponse>(book));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ModifyBookRequest bookDto)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Title = bookDto.Title;
            book.PublicationYear = bookDto.PublicationYear;
            
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}