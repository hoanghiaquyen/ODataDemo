using Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataDemo.ODataQuery
{
    public class BooksModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider provider)
        {
            var builder = new ODataConventionModelBuilder(provider);

            builder.EntitySet<Book>(nameof(Book))
                            .EntityType
                            .Filter() // Allow for the $filter Command
                            .Count() // Allow for the $count Command
                            .Expand() // Allow for the $expand Command
                            .OrderBy() // Allow for the $orderby Command
                            .Page(100, 100) // Allow for the $top and $skip Commands
                            .Select();// Allow for the $select Command; 

            builder.EntitySet<Author>(nameof(Author))
                            .EntityType
                            .Filter() // Allow for the $filter Command
                            .Count() // Allow for the $count Command
                            .Expand() // Allow for the $expand Command
                            .OrderBy() // Allow for the $orderby Command
                            .Page(100, 100) // Allow for the $top and $skip Commands
                            .Select() // Allow for the $select Command
                            .ContainsMany(x => x.Books)
                            .Expand();

            builder.EntitySet<Publisher>(nameof(Publisher))
                            .EntityType
                            .Filter() // Allow for the $filter Command
                            .Count() // Allow for the $count Command
                            .Expand() // Allow for the $expand Command
                            .OrderBy() // Allow for the $orderby Command
                            .Page(100, 100) // Allow for the $top and $skip Commands
                            .Select() // Allow for the $select Command
                            .HasMany(x => x.Books)
                            .Expand();

            return builder.GetEdmModel();
        }
    }
}
