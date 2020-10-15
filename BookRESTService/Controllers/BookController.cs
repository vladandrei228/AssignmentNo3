using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_1;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookRESTService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>()
        {
            new Book("Heidi","Johanna Spyri", 365, "1-1234567-001"),
            new Book("Rich Dad, Poor Dad","Robert Kiyosaki", 215, "1-1234567-093"),
            new Book("Increase your finaincial IQ","Robert Kiyosaki", 202, "1-1234567-094"),
            new Book("Reach Kid, Smart Kid","Robert Kiyosaki", 165, "1-1234567-095"),
            new Book("CashFlow Quadrant","Robert Kiyosaki", 175, "1-1234567-097"),
            new Book("ABCs of real estate investing","Robert Kiyosaki", 305, "1-1234567-098"),
            new Book("Why the rich are getting richer","Robert Kiyosaki", 225, "1-1234567-099")
        };
        // GET: api/<BookController>
        [HttpGet]
        public List<Book> Get()
        {
            return books;
        }

        // GET api/<BookController>/5
        [HttpGet("{Isbn13}")]
        public IActionResult Get(string Isbn13)
        {
            var book = GetBook(Isbn13);
            if (book == null)
            {
                return NotFound(new {message = "Book not found"});
            }

            return Ok(book);
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (!BookExists(book.isbn13))
            {
                books.Add(book);
                return CreatedAtAction("Get", new { id = book.isbn13 }, book);
            }
            else
            {
                return NotFound(new { message = "Isbn is duplicated" });
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{Isbn13}")]
        public IActionResult Put(string Isbn13, [FromBody] Book newBook)
        {
            if (Isbn13 != newBook.isbn13)
            {
                return BadRequest();
            }
            var currentBook = GetBook(Isbn13);

            if (currentBook != null)
            {
                currentBook.isbn13 = newBook.isbn13;
                currentBook.author = newBook.author;
                currentBook.title = newBook.title;
                currentBook.pgnr = newBook.pgnr;
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{Isbn13}")]
        public IActionResult Delete(string Isbn13)
        {
            var book = GetBook(Isbn13);

            if (book != null)
            {
                books.Remove(book);
            }
            else
            {
                return NotFound();
            }
            return Ok(book);
        }

        public Book GetBook(string isbn13)
        {
            var book = books.FirstOrDefault(e => e.isbn13 == isbn13);
            return book;
        }
        private bool BookExists(string isbn13)
        {
            return books.Any(e => e.isbn13 == isbn13);
        }
    }
}
