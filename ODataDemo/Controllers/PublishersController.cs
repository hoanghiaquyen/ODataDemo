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
    //public class PublishersController : ODataController
    public class PublisherController : ODataController
    {
        private readonly BookDbContext context;

        public PublisherController(BookDbContext context) => this.context = context;

        // GET: api/publishers
        //[HttpGet]
        //[EnableQuery]
        //public IEnumerable<Publisher> Get() => context.Publishers.Include(b => b.Books).ThenInclude(b => b.Author);

        // GET: odata/Publisher
        [EnableQuery]
        public IQueryable<Publisher> Get() => context.Publishers.AsQueryable();
    }
}