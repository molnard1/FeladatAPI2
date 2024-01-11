using AutoMapper;
using FeladatAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeladatAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    // We don't want to use the base type here, it breaks DI
    public class AuthorsController(BookContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAuthorResponse>>> Get()
        {
            var authors = await context.Authors.ToListAsync();
            return Ok(mapper.Map<List<GetAuthorResponse>>(authors));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetAuthorResponse>> Get(Guid id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            return Ok(mapper.Map<GetAuthorResponse>(author));
        }

        [HttpPost]
        public async Task<ActionResult<GetAuthorResponse>> Post([FromBody] CreateAuthorRequest authorDto)
        {
            var author = mapper.Map<Author>(authorDto);
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            
            return CreatedAtAction("Get", new { id = author.AuthorId }, mapper.Map<GetAuthorResponse>(author));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ModifyAuthorRequest authorDto)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("/nationality/{nationality:guid}")]
        public async Task<ActionResult<GetAuthorResponse>> GetByNat(Guid nationality)
        {
            var author = await context.Authors.Where(x => x.NationalityId == nationality).ToListAsync();
            return Ok(mapper.Map<List<GetAuthorResponse>>(author));
        }
    }
}