using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataExample.Models;

namespace ODataExample.Controllers
{
    public class BooksController : ODataController
    {
        private readonly BookStoreContext _context;

        public BooksController(BookStoreContext context)
        {
            _context = context;
            if (!context.Books.Any())
            {
                foreach (var book in DataSource.GetBooks())
                {
                    context.Books.Add(book);
                    context.Presses.Add(book.Press);
                }
                context.SaveChanges();
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Books);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_context.Books.FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody]Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return Created(book);
        }
    }
}
