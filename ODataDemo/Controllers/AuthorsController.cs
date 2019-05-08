using System;
using System.Collections.Generic;
using System.Linq;
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
        [EnableQuery]
        public IQueryable<Author> Get() => context.Authors.AsQueryable();
        //public IEnumerable<Author> Get() => context.Authors.Include(x=>x.Books).ToList();

    }
}