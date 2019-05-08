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
    //public class BooksController : ODataController
    public class BookController : ODataController
    {
        private readonly BookDbContext context;

        public BookController(BookDbContext context) => this.context = context;

        // GET: api/books
        //[HttpGet]
        //[EnableQuery]
        //public IEnumerable<Book> Get() => context.Books.Include(b => b.Author).Include(b => b.Publisher);

        // GET: odata/Book
        [EnableQuery]
        public IQueryable<Book> Get() => context.Books.AsQueryable();
    }
}