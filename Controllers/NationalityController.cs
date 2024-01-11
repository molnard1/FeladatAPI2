using FeladatAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeladatAPI.Controllers
{
    [Route("api/nationalities")]
    [ApiController]
    public class NationalityController(BookContext context) : ControllerBase
    {
        // GET: api/<NationalityController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await context.Nationalities.ToListAsync());
        }

        // GET api/<NationalityController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var nationality = await context.Nationalities.FindAsync(id);
            if (nationality == null)
            {
                return NotFound();
            }

            return Ok(nationality);
        }

        // POST api/<NationalityController>
        [HttpPost]
        public async Task<ActionResult> Post(string value)
        {
            var nat = new Nationality
            {
                Id = Guid.NewGuid(),
                Name = value
            };

            await context.Nationalities.AddAsync(nat);
            await context.SaveChangesAsync();

            return Ok(nat);
        }

        // PUT api/<NationalityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, string value)
        {
            var nationality = await context.Nationalities.FindAsync(id);
            if (nationality == null)
            {
                return NotFound();
            }

            nationality.Name = value;
            await context.SaveChangesAsync();

            return Ok(nationality);
        }

        // DELETE api/<NationalityController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var nationality = await context.Nationalities.FindAsync(id);
            if (nationality == null)
            {
                return NotFound();
            }

            context.Nationalities.Remove(nationality);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
