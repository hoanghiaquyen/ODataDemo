using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data.Context;
using Data.Entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ODataDemo.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    //[ApiController]
    //public class AuthorsController : ODataController
    public class AuthorController : ODataController
    {
        private readonly BookDbContext context;

        public AuthorController(BookDbContext context) => this.context = context;

        // GET: api/Authors
        //[HttpGet]
        //public IEnumerable<Author> Get() => context.Authors.Include(b => b.Books).ThenInclude(b => b.Publisher);

        // GET: odata/Author
        [HttpGet]
        [EnableQuery]
        public IQueryable<Author> Get() => context.Authors.AsQueryable();
        //public IEnumerable<Author> Get() => context.Authors.Include(x=>x.Books).ToList();

        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return Created(author);
        }

        [HttpPut]
        [EnableQuery]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody]Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != author.Id)
            {
                return BadRequest();
            }
            context.Entry(author).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(author);
        }

        [HttpDelete("{key}")]
        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var product = await context.Authors.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            context.Authors.Remove(product);
            await context.SaveChangesAsync();
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        private async Task<bool> AuthorExists(int key)
        {
            return await context.Authors.AnyAsync(x => x.Id.Equals(key));
        }
    }
}